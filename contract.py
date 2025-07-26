from seahorse.prelude import *

declare_id('7LDHuor4Srmu9KG92PomLuDy8nXebNF545nQ5qXURrgz')

# Campaign
class Campaign(Account):
  owner: Pubkey
  created_at: i64
  target: u64
  deadline: i64 
  amount_collected: u64
  amount_withdrawable: u64
  withdrawable: bool
  status: u8

# enum CampaignStatus:
#   0 = Active
#   1 = Ended
#   2 = Withdrawn

class Donation(Account):
  donor: Pubkey
  amount_collected : u64
  campaign: Pubkey
  status: u8

# enum DonateStatus:
#   0 = Donate
#   1 = Refund

class Transaction(Account):
  donor: Pubkey
  amount: u64
  timestamp: i64
  campaign: Pubkey
  transaction_type: u8

# enum TransactionType:
#   0 = Donate
#   1 = Refund
#   2 = Withdraw

# initialize campaign
@instruction
def init_campaign(
  owner: Signer, 
  campaign: Empty[Campaign], 
  project_id: str,
  target: u64,
  deadline: i64,
  clock: Clock
):
  assert deadline > clock.unix_timestamp(), "Deadline must be in the future"
  assert target > 0, "Target must be greater than 0"

  campaign = campaign.init(
    payer = owner,
    seeds = ['campaign', project_id]
  )
  
  campaign.owner = owner.key()
  campaign.created_at = clock.unix_timestamp()
  campaign.target = target
  campaign.deadline = deadline
  campaign.amount_collected = 0
  campaign.amount_withdrawable = 0
  campaign.withdrawable = False
  campaign.status = 0


@instruction
def donate_campaign(
  donor: Signer,
  campaign: Campaign,
  donation: Empty[Donation],
  transaction: Empty[Transaction],
  amount: u64,
  timestamp: i64,
  clock: Clock
):
  assert amount > 0, "Amount must be greater than zero"
  assert clock.unix_timestamp() < campaign.deadline and campaign.status == 0, "Campaign deadline passed"

  donation = donation.init(
    payer = donor,
    seeds = ['donation', campaign.key(), donor.key()]
  )

  donation.donor = donor.key()
  donation.amount_collected = amount
  donation.campaign = campaign.key()
  donation.status = 0

  transaction = transaction.init(
    payer = donor,
    seeds = ['transaction', campaign.key(), donor.key(), timestamp]
  )

  transaction.donor = donor.key()
  transaction.amount = amount
  transaction.timestamp = timestamp
  transaction.campaign = campaign.key()
  transaction.transaction_type = 0

  campaign.amount_collected += amount
  campaign.amount_withdrawable += amount

  if campaign.amount_collected => campaign.target
    campaign.withdrawable = True

  donor.transfer_lamports(to = campaign, amount = amount)


@instruction
def exist_donate_campaign(
  donor: Signer,
  campaign: Campaign,
  donation: Donation,
  transaction: Empty[Transaction],
  amount: u64,
  timestamp: i64,
  clock: Clock
):
  assert amount > 0, "Amount must be greater than zero"
  assert clock.unix_timestamp() < campaign.deadline and campaign.status == 0, "Campaign deadline passed"
  assert donation.donor == donor.key(), "Only the original donor can donate"

  transaction = transaction.init(
    payer = donor,
    seeds = ['transaction', campaign.key(), donor.key(), timestamp]
  )

  transaction.donor = donor.key()
  transaction.amount = amount
  transaction.timestamp = clock.unix_timestamp()
  transaction.campaign = campaign.key()
  transaction.transaction_type = 0
  
  donor.transfer_lamports(to = campaign, amount = amount)

  donation.amount_collected += amount

  campaign.amount_collected += amount
  campaign.amount_withdrawable += amount

  if campaign.amount_collected => campaign.target
    campaign.withdrawable = True


@instruction
def refund_campaign(
  donor: Signer,
  campaign: Campaign,
  donation: Donation,
  transaction: Empty[Transaction],
  timestamp: i64,
  clock: Clock
):
  assert donation.amount_collected > 0, "Nothing to refund"
  assert donation.donor == donor.key(), "Only the original donor can withdraw"
  assert clock.unix_timestamp() > campaign.deadline, "Cannot refund - campaign not ended"
  assert campaign.status == 1, "Cannot refund - status does not allow this"

  transaction = transaction.init(
    payer = donor,
    seeds = ['transaction', campaign.key(), donor.key(), timestamp]
  )

  transaction.donor = donor.key()
  transaction.amount = donation.amount_collected
  transaction.timestamp = clock.unix_timestamp()
  transaction.campaign = campaign.key()
  transaction.transaction_type = 1

  campaign.transfer_lamports(to = donor, amount = donation.amount_collected)

  campaign.amount_withdrawable -= donation.amount_collected

  donation.status = 1
  donation.amount_collected = 0
  


@instruction
def withdraw_campaign(
  owner: Signer,
  campaign: Campaign,
  transaction: Empty[Transaction],
  timestamp: i64,
  clock: Clock
):
  assert campaign.withdrawnable > 0, "Nothing to withdraw"
  assert campaign.owner == owner.key(), "Only the owner can withdraw"
  assert campaign.amount_collected >= campaign.target, "Target not reached yet"
  assert campaign.status != 2, "Already withdrawn"

  transaction = transaction.init(
    payer = owner,
    seeds = ['transaction', campaign.key(), owner.key(), timestamp]
  )

  transaction.donor = owner.key()
  transaction.amount = campaign.amount_collected
  transaction.timestamp = clock.unix_timestamp()
  transaction.campaign = campaign.key()
  transaction.transaction_type = 2

  campaign.transfer_lamports(
    to = owner,
    amount = campaign.amount_collected
  )

  campaign.status = 2
  campaign.amount_withdrawable = 0


@instruction
def check_campaign(campaign: Campaign, clock: Clock):
  if (
    clock.unix_timestamp() > campaign.deadline
    and campaign.amount_collected < campaign.target
    and campaign.status == 0
  ):
    campaign.status = 1

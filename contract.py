from seahorse.prelude import *

declare_id('FK5CCrit47WFvBxkrLPUxcKGUd2Mr7x3wZysT3mRFJ1A')

# Campaign
class Campaign(Account):
  owner: Pubkey
  created_at: i64
  target: u64
  deadline: i64 
  amount_collected: u64
  status: u8

# enum CampaignStatus:
#   0 = Active
#   1 = Ended
#   2 = Withdrawn

class Donation(Account):
  donor: Pubkey
  amount: u64
  timestamp: i64
  campaign: Pubkey
  donation_type: u8

# enum DonateType:
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
  campaign.status = 0


@instruction
def donate_campaign(
  donor: Signer,
  campaign: Campaign,
  donation: Empty[Donation],
  amount: u64,
  timestamp: i64,
  clock: Clock
):
  assert clock.unix_timestamp() < campaign.deadline and campaign.status == 0, "Campaign deadline passed"

  donor.transfer_lamports(to = campaign, amount = amount)

  donation = donation.init(
    payer = donor,
    seeds = ['donation', campaign.key(), donor.key(), timestamp]
  )

  donation.donor = donor.key()
  donation.amount = amount
  donation.timestamp = timestamp
  donation.campaign = campaign.key()
  donation.donation_type = 0

  campaign.amount_collected += amount


@instruction
def refund_campaign_test(
  donor: Signer,
  campaign: Campaign,
  donation: Empty[Donation],
  amount: u64,
  timestamp: i64,
  clock: Clock
):
  assert clock.unix_timestamp() > campaign.deadline, "Cannot refund"
  assert campaign.status == 1, "Cannot refund"

  donor.transfer_lamports(to = campaign, amount = amount)

  donation = donation.init(
    payer = donor,
    seeds = ['donation', campaign.key(), donor.key()]
  )

  donation.donor = donor.key()
  donation.amount = amount
  donation.timestamp = clock.unix_timestamp()
  donation.campaign = campaign.key()
  donation.donation_type = 1

  campaign.amount_collected += amount


@instruction
def refund_campaign(
  donor: Signer,
  campaign: Campaign,
  donation: Donation,
  amount: u64,
  timestamp: i64,
  clock: Clock
):
  assert donation.donor == donor.key(), "Only the owner can withdraw"
  assert clock.unix_timestamp() > campaign.deadline, "Cannot refund - not and"
  assert campaign.status == 1, "Cannot refund"

  campaign.transfer_lamports(to = donor, amount = donation.amount)

  donation.donation_type = 1

  campaign.amount_collected -= amount


@instruction
def withdraw_campaign(
  owner: Signer,
  campaign: Campaign,
  donation: Empty[Donation],
  timestamp: i64,
  clock: Clock
):
  assert campaign.owner == owner.key(), "Only the owner can withdraw"
  assert campaign.amount_collected >= campaign.target, "Target not reached yet"
  assert campaign.status != 2, "Already withdrawn"

  donation = donation.init(
    payer = owner,
    seeds = ['donation', campaign.key(), owner.key()]
  )

  donation.donation_type = 2

  campaign.transfer_lamports(
    to = owner,
    amount = campaign.amount_collected
  )
  donation.donor = owner.key()
  donation.amount = campaign.amount_collected
  donation.timestamp = clock.unix_timestamp()
  donation.campaign = campaign.key()
  donation.donation_type = 2

  campaign.status = 2
  campaign.amount_collected = 0


@instruction
def check_campaign(campaign: Campaign, clock: Clock):
  if clock.unix_timestamp() > campaign.deadline and campaign.amount_collected < campaign.target:
    campaign.status = 1

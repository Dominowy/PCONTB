from seahorse.prelude import *

declare_id('DtmqwdxpS1QnujyRV7VoiAg6a73P6ryFSKJE7qiFq99B')

# Campaign
class Campaign(Account):
  owner: Pubkey
  created_at: i64
  target: u64
  deadline: i64 
  amount_collected: u64


class Donation(Account):
  donor: Pubkey
  amount: u64
  timestamp: i64
  campaign: Pubkey

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
  assert target > 0, "Deadline must be in the future"

  campaign = campaign.init(
    payer = owner,
    seeds = ['campaign', project_id]
  )
  
  campaign.owner = owner.key()
  campaign.created_at = clock.unix_timestamp()
  campaign.target = target
  campaign.deadline = deadline
  campaign.amount_collected = 0


@instruction
def donate_campaign(
  donor: Signer,
  campaign: Campaign,
  donation: Empty[Donation],
  amount: u64,
  timestamp: i64,
  clock: Clock
):
  assert clock.unix_timestamp() < campaign.deadline, "Campaign deadline passed"

  donor.transfer_lamports(to = campaign, amount = amount)

  donation = donation.init(
    payer = donor,
    seeds = ['donation', campaign.key(), donor.key(), timestamp]
  )

  donation.donor = donor.key()
  donation.amount = amount
  donation.timestamp = clock.unix_timestamp()
  donation.campaign = campaign.key()

  campaign.amount_collected += amount


@instruction
def withdraw_campaign(
  owner: Signer,
  campaign: Campaign,
  clock: Clock
):
  assert campaign.owner == owner.key(), "Only the owner can withdraw"
  assert campaign.amount_collected >= campaign.target, "Target not reached yet"
  assert clock.unix_timestamp() <= campaign.deadline, "Campaign expired"

  campaign.transfer_lamports(
    to = owner,
    amount = campaign.amount_collected
  )

  campaign.amount_collected = 0
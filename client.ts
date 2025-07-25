const projectId = 'abc128';
const target = new BN(1_000_000_000); // 1 SOL
const deadline = Math.floor(Date.now() / 1000) + 86400; // 24h od teraz
const amount = new BN(500_000_000);
const timestamp = Math.floor(Date.now() / 1000);

const [campaignPda] = web3.PublicKey.findProgramAddressSync([Buffer.from('campaign'), Buffer.from(projectId)], pg.PROGRAM_ID);

const [donationPda] = web3.PublicKey.findProgramAddressSync(
	[Buffer.from('donation'), campaignPda.toBuffer(), pg.wallet.publicKey.toBuffer(), Buffer.from(new BN(timestamp).toArray('le', 8))],
	pg.PROGRAM_ID
);

try {
	const tx = await pg.program.methods
		.initCampaign(projectId, target, new BN(deadline))
		.accounts({
			owner: pg.wallet.publicKey,
			campaign: campaignPda,
		})
		.rpc();
	console.log(`Use 'solana confirm -v ${tx}' to see the logs`);
} catch (e) {
	console.error('TX failed:', e.logs ?? e.message);
}

try {
	const tx = await pg.program.methods
		.donateCampaign(amount, new BN(timestamp))
		.accounts({
			donor: pg.wallet.publicKey,
			campaign: campaignPda,
			donation: donationPda,
		})
		.rpc();
	console.log(`Use 'solana confirm -v ${tx}' to see the logs`);
} catch (e) {
	console.error('TX failed:', e.logs ?? e.message);
}

try {
	const tx = await pg.program.methods
		.withdrawCampaign()
		.accounts({
			owner: pg.wallet.publicKey,
			campaign: campaignPda,
		})
		.rpc();

	console.log(`💰 Withdrawal successful! TX: ${tx}`);
} catch (e) {
	console.error(e);
}

try {
	const campaign = await pg.program.account.campaign.fetch(campaignPda);

	console.log('📋 Campaign info:');
	console.log('Owner:', campaign.owner.toBase58());
	console.log('Target:', campaign.target.toString());
	console.log('Collected:', campaign.amountCollected.toString());
	console.log('Deadline:', new Date(campaign.deadline.toNumber() * 1000));
} catch (e) {
	console.error(e);
}

try {
	const donations = await pg.program.account.donation.all([
		{
			memcmp: {
				offset: 56,
				bytes: campaignPda.toBase58(),
			},
		},
	]);

	for (const donation of donations) {
		console.log('Donor:', donation.account.donor.toBase58());
		console.log('Amount:', donation.account.amount.toString());
		console.log('Timestamp:', new Date(donation.account.timestamp.toNumber() * 1000));
	}
} catch (e) {
	console.error(e);
}

import { defineStore } from "pinia";
import { blockchains } from "@/blockchain/blockchainManager";
import solanaClient from "@/blockchain/solanaClient";

import { Buffer } from "buffer";
import { BN } from "@project-serum/anchor";
import { PublicKey } from "@solana/web3.js";

export const useWalletStore = defineStore("wallet", {
  state: () => ({
    address: null,
    connected: false,
    blockchain: "solana",
  }),

  actions: {
    async connect() {
      try {
        const address = await blockchains[this.blockchain].connect();
        this.address = address;
        this.connected = true;
      } catch (e) {
        console.error("Błąd łączenia z portfelem:", e.message);
      }
    },

    disconnect() {
      this.address = null;
      this.connected = false;
    },

    async initCampaign() {
      try {
        const program = solanaClient.getProgram();

        const projectId = "23453236";
        const target = new BN(1_000_000_000); // 1 SOL
        const deadline = new BN(Math.floor(Date.now() / 1000) + 60); // 24h od teraz

        const [campaignPda] = await PublicKey.findProgramAddress(
          [Buffer.from("campaign"), Buffer.from(projectId)],
          solanaClient.PROGRAM_ID
        );

        const tx = await program.methods
          .initCampaign(projectId, target, deadline)
          .accounts({
            owner: new PublicKey(this.address),
            campaign: campaignPda,
          })
          .rpc();

        const confirmed = await solanaClient.connection.confirmTransaction(tx, "confirmed");
        console.log("Potwierdzono:", confirmed);

        console.log("Transaction hash:", tx);
      } catch (e) {
        console.error("initCampaign failed:", e);
      }
    },
    async donateCampaign() {
      try {
        const program = solanaClient.getProgram();

        const projectId = "23453236";
        const amount = new BN(500_000_000);
        const timestamp = Math.floor(Date.now() / 1000);

        const [campaignPda] = await PublicKey.findProgramAddress(
          [Buffer.from("campaign"), Buffer.from(projectId)],
          solanaClient.PROGRAM_ID
        );

        const [donationPda] = await PublicKey.findProgramAddress(
          [
            Buffer.from("donation"),
            campaignPda.toBuffer(),
            new PublicKey(this.address).toBuffer(),
            Buffer.from(new BN(timestamp).toArray("le", 8)),
          ],
          solanaClient.PROGRAM_ID
        );

        const tx = await program.methods
          .donateCampaign(amount, new BN(timestamp))
          .accounts({
            donor: new PublicKey(this.address),
            campaign: campaignPda,
            donation: donationPda,
          })
          .rpc();

        // Poczekaj na potwierdzenie transakcji
        const confirmed = await solanaClient.connection.confirmTransaction(tx, "confirmed");

        console.log("Potwierdzono:", confirmed);
        console.log("Transaction hash:", tx);
      } catch (e) {
        console.error("donateCampaign failed:", e);
      }
    },
    async withdrawCampaign() {
      try {
        const program = solanaClient.getProgram();

        const timestamp = Math.floor(Date.now() / 1000);
        const projectId = "23453236";

        const [campaignPda] = await PublicKey.findProgramAddress(
          [Buffer.from("campaign"), Buffer.from(projectId)],
          solanaClient.PROGRAM_ID
        );

        const [donationPda] = await PublicKey.findProgramAddress(
          [Buffer.from("donation"), campaignPda.toBuffer(), new PublicKey(this.address).toBuffer()],
          solanaClient.PROGRAM_ID
        );

        const tx = await program.methods
          .withdrawCampaign(new BN(timestamp))
          .accounts({
            owner: new PublicKey(this.address),
            campaign: campaignPda,
            donation: donationPda,
          })
          .rpc();

        console.log(`💰 Withdrawal successful! TX: ${tx}`);
      } catch (e) {
        console.error("withdrawCampaign failed:", e);
      }
    },
    async getCampaignDetails() {
      const program = solanaClient.getProgram();

      const projectId = "23453236";

      const [campaignPda] = await PublicKey.findProgramAddress(
        [Buffer.from("campaign"), Buffer.from(projectId)],
        solanaClient.PROGRAM_ID
      );

      const campaign = await program.account.campaign.fetch(campaignPda);

      console.log(campaign);
    },
    async getCampaignTransactions() {
      const program = solanaClient.getProgram();

      const projectId = "23453236";

      const [campaignPda] = await PublicKey.findProgramAddress(
        [Buffer.from("campaign"), Buffer.from(projectId)],
        solanaClient.PROGRAM_ID
      );

      const donations = await program.account.donation.all([
        {
          memcmp: {
            offset: 56,
            bytes: campaignPda.toBase58(),
          },
        },
      ]);

      console.log(donations);
    },
    async refundAllForWallet() {
      const program = solanaClient.getProgram();

      const projectId = "23453236";

      const [campaignPda] = await PublicKey.findProgramAddress(
        [Buffer.from("campaign"), Buffer.from(projectId)],
        solanaClient.PROGRAM_ID
      );

      await program.methods
        .checkCampaign()
        .accounts({
          campaign: campaignPda,
        })
        .rpc();

      const donations = await program.account.donation.all([
        {
          memcmp: {
            offset: 8,
            bytes: new PublicKey(this.address).toBase58(),
          },
        },
      ]);

      console.log(donations);

      for (const donationData of donations) {
        try {
          const { amount, timestamp, campaign } = donationData.account;

          const tx = await program.methods
            .refundCampaign(new BN(amount), new BN(timestamp))
            .accounts({
              donor: new PublicKey(this.address),
              campaign: new PublicKey(campaign),
              donation: donationData.publicKey,
            })
            .rpc();

          console.log(`Refunded donation ${donationData.publicKey.toBase58()} TX: ${tx}`);
        } catch (e) {
          console.error(`Refund failed for ${donationData.publicKey.toBase58()}:`, e);
        }
      }
    },
  },
});

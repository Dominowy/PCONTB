import { defineStore } from "pinia";
import { blockchains } from "@/blockchain/blockchainManager";
import solanaClient from "@/blockchain/solanaClient";

import { Buffer } from "buffer";
import { BN } from "@project-serum/anchor";
import { PublicKey } from "@solana/web3.js";

export const useWalletStore = defineStore("wallet", {
  state: () => ({
    address: localStorage.getItem("walletAddress") || null,
    connected: !!localStorage.getItem("walletAddress"),
    blockchain: localStorage.getItem("blockchain") || "solana",
  }),

  actions: {
    async connect() {
      try {
        const address = await blockchains[this.blockchain].connect();
        this.address = address;
        this.connected = true;

        localStorage.setItem("walletAddress", address);
        localStorage.setItem("blockchain", this.blockchain);
      } catch (e) {
        console.error("Błąd łączenia z portfelem:", e.message);
      }
    },
    async loadFromStorage() {
      const storedAddress = localStorage.getItem("walletAddress");
      const storedBlockchain = localStorage.getItem("blockchain");

      if (storedAddress && storedBlockchain) {
        this.address = storedAddress;
        this.blockchain = storedBlockchain;
        this.connected = true;
      }
    },
    async disconnect() {
      this.address = null;
      this.connected = false;
      localStorage.removeItem("walletAddress");
      localStorage.removeItem("blockchain");
    },

    async initCampaign(projectId, target, deadline) {
      try {
        const program = solanaClient.getProgram();

        const cleanId = projectId.replace(/-/g, "");

        const [campaignPda] = await PublicKey.findProgramAddress(
          [Buffer.from("campaign"), Buffer.from(cleanId)],
          solanaClient.PROGRAM_ID
        );

        const tx = await program.methods
          .initCampaign(cleanId, target, deadline)
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
    async donateCampaign(projectId, amount) {
      try {
        const program = solanaClient.getProgram();

        const cleanId = projectId.replace(/-/g, "");

        const timestamp = Math.floor(Date.now() / 1000);

        const [campaignPda] = await PublicKey.findProgramAddress(
          [Buffer.from("campaign"), Buffer.from(cleanId)],
          solanaClient.PROGRAM_ID
        );

        const [donationPda] = await PublicKey.findProgramAddress(
          [Buffer.from("donation"), campaignPda.toBuffer(), new PublicKey(this.address).toBuffer()],
          solanaClient.PROGRAM_ID
        );

        const [transactionPda] = await PublicKey.findProgramAddress(
          [
            Buffer.from("transaction"),
            campaignPda.toBuffer(),
            new PublicKey(this.address).toBuffer(),
            Buffer.from(new BN(timestamp).toArray("le", 8)),
          ],
          solanaClient.PROGRAM_ID
        );

        const donationAccount = await solanaClient.connection.getAccountInfo(donationPda);

        let tx;

        if (donationAccount === null) {
          tx = await program.methods
            .donateCampaign(amount, new BN(timestamp))
            .accounts({
              donor: new PublicKey(this.address),
              campaign: campaignPda,
              donation: donationPda,
              transaction: transactionPda,
            })
            .rpc();
        } else {
          tx = await program.methods
            .existDonateCampaign(amount, new BN(timestamp))
            .accounts({
              donor: new PublicKey(this.address),
              campaign: campaignPda,
              donation: donationPda,
              transaction: transactionPda,
            })
            .rpc();
        }

        const confirmed = await solanaClient.connection.confirmTransaction(tx, "confirmed");

        console.log("Potwierdzono:", confirmed);
        console.log("Transaction hash:", tx);
      } catch (e) {
        console.error("donateCampaign failed:", e);
      }
    },
    async withdrawCampaign(projectId) {
      try {
        const program = solanaClient.getProgram();

        const cleanId = projectId.replace(/-/g, "");

        const timestamp = Math.floor(Date.now() / 1000);

        const [campaignPda] = await PublicKey.findProgramAddress(
          [Buffer.from("campaign"), Buffer.from(cleanId)],
          solanaClient.PROGRAM_ID
        );

        const [transactionPda] = await PublicKey.findProgramAddress(
          [
            Buffer.from("transaction"),
            campaignPda.toBuffer(),
            new PublicKey(this.address).toBuffer(),
            Buffer.from(new BN(timestamp).toArray("le", 8)),
          ],
          solanaClient.PROGRAM_ID
        );

        const tx = await program.methods
          .withdrawCampaign(new BN(timestamp))
          .accounts({
            owner: new PublicKey(this.address),
            campaign: campaignPda,
            transaction: transactionPda,
          })
          .rpc();

        const confirmed = await solanaClient.connection.confirmTransaction(tx, "confirmed");
        console.log("Potwierdzono:", confirmed);

        console.log(`💰 Withdrawal successful! TX: ${tx}`);
      } catch (e) {
        console.error("withdrawCampaign failed:", e);
      }
    },
    async getCampaignDetails(projectId) {
      const program = solanaClient.getProgram();

      const cleanId = projectId.replace(/-/g, "");

      const [campaignPda] = await PublicKey.findProgramAddress(
        [Buffer.from("campaign"), Buffer.from(cleanId)],
        solanaClient.PROGRAM_ID
      );

      try {
        let isExist = await solanaClient.connection.getAccountInfo(campaignPda);

        if (!isExist) {
          return null;
        }

        const campaign = await program.account.campaign.fetch(campaignPda);

        return campaign;
      } catch (error) {
        console.error(error);
      }
    },
    async getCampaignTransactions(projectId) {
      const program = solanaClient.getProgram();

      const cleanId = projectId.replace(/-/g, "");

      const [campaignPda] = await PublicKey.findProgramAddress(
        [Buffer.from("campaign"), Buffer.from(cleanId)],
        solanaClient.PROGRAM_ID
      );

      try {
        let isExist = await solanaClient.connection.getAccountInfo(campaignPda);

        if (!isExist) {
          return null;
        }

        const transactions = await program.account.transaction.all([
          {
            memcmp: {
              offset: 56,
              bytes: campaignPda.toBase58(),
            },
          },
        ]);

        return transactions;
      } catch (error) {
        console.error(error);
      }
    },
    async getCampaignDonator(projectId) {
      const program = solanaClient.getProgram();

      const cleanId = projectId.replace(/-/g, "");

      const [campaignPda] = await PublicKey.findProgramAddress(
        [Buffer.from("campaign"), Buffer.from(cleanId)],
        solanaClient.PROGRAM_ID
      );

      const [donationPda] = await PublicKey.findProgramAddress(
        [Buffer.from("donation"), campaignPda.toBuffer(), new PublicKey(this.address).toBuffer()],
        solanaClient.PROGRAM_ID
      );

      try {
        let isExist = await solanaClient.connection.getAccountInfo(donationPda);

        if (!isExist) {
          return null;
        }

        const donator = await program.account.donation.fetch(donationPda);

        return donator;
      } catch (error) {
        console.error(error);
      }
    },
    async getCampaignDonatorTransactions(projectId) {
      const program = solanaClient.getProgram();

      const cleanId = projectId.replace(/-/g, "");

      const [campaignPda] = await PublicKey.findProgramAddress(
        [Buffer.from("campaign"), Buffer.from(cleanId)],
        solanaClient.PROGRAM_ID
      );

      const [donationPda] = await PublicKey.findProgramAddress(
        [Buffer.from("donation"), campaignPda.toBuffer(), new PublicKey(this.address).toBuffer()],
        solanaClient.PROGRAM_ID
      );

      try {
        let isExist = await solanaClient.connection.getAccountInfo(donationPda);

        if (!isExist) {
          return null;
        }

        const transactions = await program.account.transaction.all([
          {
            memcmp: {
              offset: 8,
              bytes: new PublicKey(this.address),
            },
          },
          {
            memcmp: {
              offset: 56,
              bytes: campaignPda.toBase58(),
            },
          },
        ]);

        return transactions;
      } catch (error) {
        console.error(error);
      }
    },
    async refundCampaign(projectId) {
      const program = solanaClient.getProgram();

      const cleanId = projectId.replace(/-/g, "");
      const timestamp = Math.floor(Date.now() / 1000);

      const [campaignPda] = await PublicKey.findProgramAddress(
        [Buffer.from("campaign"), Buffer.from(cleanId)],
        solanaClient.PROGRAM_ID
      );

      const [donationPda] = await PublicKey.findProgramAddress(
        [Buffer.from("donation"), campaignPda.toBuffer(), new PublicKey(this.address).toBuffer()],
        solanaClient.PROGRAM_ID
      );

      const [transactionPda] = await PublicKey.findProgramAddress(
        [
          Buffer.from("transaction"),
          campaignPda.toBuffer(),
          new PublicKey(this.address).toBuffer(),
          Buffer.from(new BN(timestamp).toArray("le", 8)),
        ],
        solanaClient.PROGRAM_ID
      );

      try {
        const tx = await program.methods
          .refundCampaign(new BN(timestamp))
          .accounts({
            donor: new PublicKey(this.address),
            campaign: campaignPda,
            donation: donationPda,
            transaction: transactionPda,
          })
          .rpc();

        const confirmed = await solanaClient.connection.confirmTransaction(tx, "confirmed");
        console.log("Potwierdzono:", confirmed);
        console.log(`Refunded donation ${donationPda.publicKey.toBase58()} TX: ${tx}`);
      } catch (e) {
        console.error(e);
      }
    },
    async checkCampaign(projectId) {
      const program = solanaClient.getProgram();

      const cleanId = projectId.replace(/-/g, "");

      const [campaignPda] = await PublicKey.findProgramAddress(
        [Buffer.from("campaign"), Buffer.from(cleanId)],
        solanaClient.PROGRAM_ID
      );

      try {
        await program.methods
          .checkCampaign()
          .accounts({
            campaign: campaignPda,
          })
          .rpc();
      } catch (error) {
        console.error(error);
      }
    },
  },
});

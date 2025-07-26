<template>
  <div>
    <template v-if="campaignInfo == null || donatorInfo == null">
      Campaign not exist or you are not donator
    </template>
    <template v-else>
      <b-card>
        <b-card-title>Owner wallet</b-card-title>
        {{ campaignInfo.owner.toBase58() }}
      </b-card>
      <b-card class="mt-2">
        <b-card-title>Donator</b-card-title>
        <p class="card-text"><strong>Wallet: </strong>{{ donatorInfo.donor.toBase58() }}</p>

        <div class="d-flex justify-content-between align-items-center">
          <div><strong>Donated: </strong>{{ formatSol(donatorInfo.amountCollected) }}</div>
          <button
            v-if="
              donatorInfo?.amountCollected > 0 &&
              (campaignInfo?.status === 1 ||
                (Math.floor(Date.now() / 1000) > campaignInfo?.deadline &&
                  campaignInfo?.amountCollected?.toNumber?.() < campaignInfo?.target?.toNumber?.()))
            "
            type="button"
            class="btn btn-primary"
            @click="refundCampaign"
          >
            Refund
          </button>
        </div>
        <h5 class="mb-3">Transactions</h5>
        <table class="table table-light table-striped table-bordered">
          <thead>
            <tr>
              <th>Date</th>
              <th>Amount</th>
              <th>Type</th>
            </tr>
          </thead>
          <tbody v-if="donatorTransactions && donatorTransactions.length > 0">
            <tr v-for="(tx, index) in donatorTransactions" :key="index">
              <td>{{ formatDate(tx.account.timestamp) }}</td>
              <td>{{ formatLamports(tx.account.amount) }}</td>
              <td>{{ getTransactionTypeLabel(tx.account.transactionType) }}</td>
            </tr>
          </tbody>
          <tbody v-else>
            <tr>
              <td colspan="3">There is no transactions to display</td>
            </tr>
          </tbody>
        </table>
      </b-card>
    </template>
  </div>
</template>

<script setup>
import { ref } from "vue";
import { useWalletStore } from "@/store/wallet";
import { onMounted } from "vue";
import { useRouter } from "vue-router";

const router = useRouter();

onMounted(async () => {
  await getCampaignInfo();
  await getDonatorInfo();
  await getDonatorTransactions();
});

const wallet = useWalletStore();
const props = defineProps({
  projectId: String,
});

const campaignInfo = ref(null);
const donatorInfo = ref(null);
const donatorTransactions = ref(null);

async function getCampaignInfo() {
  try {
    let info = await wallet.getCampaignDetails(props.projectId);

    campaignInfo.value = info;
  } catch (error) {
    console.log(error);
  }
}

async function getDonatorInfo() {
  try {
    let info = await wallet.getCampaignDonator(props.projectId);

    donatorInfo.value = info;
  } catch (error) {
    console.log(error);
  }
}

async function getDonatorTransactions() {
  try {
    let transactions = await wallet.getCampaignDonatorTransactions(props.projectId);

    donatorTransactions.value = transactions;
  } catch (error) {
    console.log(error);
  }
}

async function refundCampaign() {
  try {
    await wallet.refundCampaign(props.projectId);
  } catch (error) {
    console.log(error);
  } finally {
    router.go(0);
  }
}

function formatLamports(amount) {
  return (amount / 1_000_000_000).toFixed(4);
}

function getTransactionTypeLabel(type) {
  switch (type) {
    case 0:
      return "Donate";
    case 1:
      return "Refund";
    case 2:
      return "Withdraw";
    default:
      return "Unknown";
  }
}

function formatSol(lamportsBN) {
  return (Number(lamportsBN.toString()) / 1_000_000_000).toFixed(2);
}

function formatDate(timestamp) {
  return new Date(timestamp * 1000).toLocaleString();
}
</script>

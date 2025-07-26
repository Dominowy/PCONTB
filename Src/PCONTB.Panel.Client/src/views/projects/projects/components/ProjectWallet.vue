<template>
  <div class="container">
    <template v-if="!campaignInfo">
      <div class="mb-3">
        <label for="amount" class="form-label">Target</label>
        <input v-model="amount" type="number" class="form-control" id="amount" required />
      </div>

      <div class="mb-3">
        <label for="deadline" class="form-label">Deadline</label>
        <input
          v-model="deadline"
          type="datetime-local"
          class="form-control"
          id="deadline"
          required
        />
      </div>

      <button type="button" class="btn btn-primary" @click="initCampaign">Start</button>
    </template>
    <template v-else>
      <p class="card-text"><strong>Owner:</strong> {{ campaignInfo.owner.toBase58() }}</p>

      <p class="card-text">
        <strong>Deadline:</strong> {{ formatDate(campaignInfo.deadline.toNumber()) }}
      </p>
      <div class="mt-3">
        <label class="form-label">Progress:</label>
        <div class="progress">
          <div
            class="progress-bar bg-success"
            role="progressbar"
            :style="{ width: progress + '%' }"
            :aria-valuenow="progress"
            aria-valuemin="0"
            aria-valuemax="100"
          >
            {{ progress.toFixed(1) }}%
          </div>
        </div>
        <div class="text-end">
          {{ formatSol(campaignInfo.amountCollected) }} / {{ formatSol(campaignInfo.target) }} SOL
        </div>
      </div>
      <div v-if="campaignInfo.amountCollected.gte(campaignInfo.target) && campaignInfo.status != 2">
        <button type="button" class="btn btn-primary" @click="withdrawCampaign">Withdraw</button>
      </div>
      <div>
        <h5 class="mb-3">Transactions</h5>
        <table class="table table-light table-striped table-bordered">
          <thead>
            <tr>
              <th>Wallet</th>
              <th>Date</th>
              <th>Amount</th>
            </tr>
          </thead>
          <tbody v-if="campaignTransactions && campaignTransactions.length > 0">
            <tr v-for="(tx, index) in campaignTransactions" :key="index">
              <td>{{ shortenAddress(tx.account.donor.toBase58()) }}</td>
              <td>{{ formatDate(tx.account.timestamp) }}</td>
              <td>{{ formatLamports(tx.account.amount) }}</td>
            </tr>
          </tbody>
          <tbody v-else>
            <tr>
              <td colspan="3">There is no transactions to display</td>
            </tr>
          </tbody>
        </table>
      </div>
    </template>
  </div>
</template>

<script setup>
import { ref } from "vue";
import { useWalletStore } from "@/store/wallet";
import { BN } from "@project-serum/anchor";
import { onMounted, computed } from "vue";
import { useRouter } from "vue-router";

const router = useRouter();

onMounted(async () => {
  await getCampaignInfo();
  await getCampaignTransactions();
});

const wallet = useWalletStore();
const props = defineProps({
  projectId: String,
});

const amount = ref(0);
const deadline = ref("");
const campaignInfo = ref(null);
const campaignTransactions = ref(null);

async function getCampaignInfo() {
  try {
    let info = await wallet.getCampaignDetails(props.projectId);

    campaignInfo.value = info;
  } catch (error) {
    console.log(error);
  }
}

async function getCampaignTransactions() {
  try {
    let info = await wallet.getCampaignTransactions(props.projectId);

    campaignTransactions.value = info;
  } catch (error) {
    console.log(error);
  }
}

async function initCampaign() {
  const lamports = new BN(amount.value * 1_000_000_000);
  const deadlineTimestamp = Math.floor(new Date(deadline.value).getTime() / 1000);
  const deadlineBN = new BN(deadlineTimestamp);

  try {
    await wallet.initCampaign(props.projectId, lamports, deadlineBN);

    console.log("Campaign created");
  } catch (e) {
    console.error("Error creating campaign", e);
  } finally {
    router.go(0);
  }
}

async function withdrawCampaign() {
  try {
    await wallet.withdrawCampaign(props.projectId);

    console.log("Campaign withdrawed");
  } catch (e) {
    console.error("Error withdraw campaign", e);
  } finally {
    getCampaignInfo();
    getCampaignTransactions();
  }
}

function formatSol(lamportsBN) {
  return (Number(lamportsBN.toString()) / 1_000_000_000).toFixed(2);
}

function formatDate(timestamp) {
  const date = new Date(timestamp * 1000);
  return date.toLocaleString();
}

function shortenAddress(address) {
  return address.slice(0, 6) + "..." + address.slice(-4);
}

function formatLamports(amount) {
  return (amount / 1_000_000_000).toFixed(4);
}

const progress = computed(() => {
  if (!campaignInfo.value) return 0;
  const collected = Number(campaignInfo.value.amountCollected.toString());
  const target = Number(campaignInfo.value.target.toString());
  return target > 0 ? (collected / target) * 100 : 0;
});
</script>

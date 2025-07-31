<template>
  <div>
    <template v-if="campaignInfo == null"> Campaign not exist </template>
    <template v-else>
      <div><strong>Deadline:</strong> {{ formatDate(campaignInfo.deadline.toNumber()) }}</div>
      <div><strong>Status:</strong> {{ getCampaingStatus(campaignInfo.status) }}</div>
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
      <div v-if="!campaignInfo.withdrawable && campaignInfo.status == 0">
        <div class="mb-3">
          <label for="amount" class="form-label">Amount</label>
          <input v-model="amount" type="number" class="form-control" id="amount" required />
        </div>
        <div class="d-flex justify-content-end">
          <button type="button" class="btn btn-primary" @click="donateCampaign">Donate</button>
        </div>
      </div>
    </template>
  </div>
</template>

<script setup>
import { ref } from "vue";
import { useWalletStore } from "@/store/wallet";
import { onMounted, computed } from "vue";
import { BN } from "@project-serum/anchor";
import { useRouter } from "vue-router";

const router = useRouter();

onMounted(async () => {
  await getCampaignInfo();
});

const wallet = useWalletStore();

const props = defineProps({
  projectId: String,
});

const campaignInfo = ref(null);
const amount = ref(0);

async function getCampaignInfo() {
  try {
    let info = await wallet.getCampaignDetails(props.projectId);

    campaignInfo.value = info;
  } catch (error) {
    console.log(error);
  }
}

async function donateCampaign() {
  const lamports = new BN(amount.value * 1_000_000_000);

  try {
    await wallet.donateCampaign(props.projectId, lamports);

    console.log("Campaign donate");
  } catch (e) {
    console.error("Error donate campaign:", e);
  } finally {
    router.go(0);
  }
}

function formatSol(lamportsBN) {
  return (Number(lamportsBN.toString()) / 1_000_000_000).toFixed(2);
}

function formatDate(timestamp) {
  return new Date(timestamp * 1000).toLocaleString();
}

const progress = computed(() => {
  if (!campaignInfo.value) return 0;
  const collected = Number(campaignInfo.value.amountCollected.toString());
  const target = Number(campaignInfo.value.target.toString());
  return target > 0 ? (collected / target) * 100 : 0;
});

function getCampaingStatus(status) {
  switch (status) {
    case 0:
      return "Active";
    case 1:
      return "Ended - campaign not reach the goal";
    case 2:
      return "Withdrawn - campaign ended succesfully";
    default:
      return "Unknown";
  }
}
</script>

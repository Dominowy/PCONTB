<template>
  <b-dropdown-item @click="connectWallet">Połącz portfel</b-dropdown-item>
  <b-dropdown-item @click="initCampaign">Test init</b-dropdown-item>
  <b-dropdown-item @click="donateCampaign">Test donate</b-dropdown-item>
  <b-dropdown-item @click="refund">Test refund</b-dropdown-item>
  <b-dropdown-item @click="withdraw">Test withdraw</b-dropdown-item>
</template>

<script setup>
import { useWalletStore } from "@/store/wallet";
const wallet = useWalletStore();

function connectWallet() {
  wallet.connect();
}
function initCampaign() {
  wallet.initCampaign();
}
function donateCampaign() {
  wallet.donateCampaign();
}
function refund() {
  wallet.refundAllForWallet();
}
function withdraw() {
  wallet.withdrawCampaign();
}
</script>

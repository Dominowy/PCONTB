<template>
  <b-button v-if="!wallet.connected" @click="connectWallet" variant="primary"> Connect </b-button>
  <template v-else>
    <div class="text-white d-flex justify-content-center align-items-center">
      <em class="me-3">
        {{ shortAddress(wallet.address) }}
      </em>
      <b-button class="me-3" @click="disconnecttWallet" variant="primary"> Disconnect </b-button>
    </div>
  </template>
</template>

<script setup>
import { useWalletStore } from "@/store/wallet";
const wallet = useWalletStore();

function connectWallet() {
  wallet.connect();
}

function disconnecttWallet() {
  wallet.disconnect();
}

function shortAddress(address) {
  if (!address || address.length < 10) return address;
  return `${address.slice(0, 6)}...${address.slice(-6)}`;
}
</script>

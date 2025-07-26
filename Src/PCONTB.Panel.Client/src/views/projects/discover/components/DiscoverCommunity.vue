<template>
  <div>
    <!-- Formularz dodawania nowej wiadomości -->
    <form @submit.prevent="submitForm" class="mb-4">
      <div class="mb-3">
        <label for="title" class="form-label">Title</label>
        <input
          id="title"
          v-model="form.title"
          type="text"
          class="form-control"
          :class="{ 'is-invalid': errors.title }"
          required
        />
        <div class="invalid-feedback" v-if="errors.title">{{ errors.title }}</div>
      </div>

      <div class="mb-3">
        <label for="message" class="form-label">Message</label>
        <textarea
          id="message"
          v-model="form.message"
          class="form-control"
          rows="3"
          :class="{ 'is-invalid': errors.message }"
          required
        ></textarea>
        <div class="invalid-feedback" v-if="errors.message">{{ errors.message }}</div>
      </div>

      <div class="mb-3">
        <label for="wallet" class="form-label">Wallet Address</label>
        <input
          id="wallet"
          v-model="form.walletAddress"
          type="text"
          class="form-control"
          :class="{ 'is-invalid': errors.walletAddress }"
          required
        />
        <div class="invalid-feedback" v-if="errors.walletAddress">{{ errors.walletAddress }}</div>
      </div>

      <button type="submit" class="btn btn-primary">Add Message</button>
    </form>

    <!-- Lista wiadomości -->
    <ul class="list-group">
      <CommunityMessageItem v-for="msg in messages" :key="msg.id" :message="msg" />
    </ul>
  </div>
</template>

<script setup>
import { ref, reactive } from "vue";

// Rekurencyjny komponent do pojedynczej wiadomości i jej reply
const CommunityMessageItem = {
  name: "CommunityMessageItem",
  props: {
    message: {
      type: Object,
      required: true,
    },
  },
  template: `
    <li class="list-group-item">
      <h5>{{ message.Title }}</h5>
      <p>{{ message.Message }}</p>
      <small class="text-muted">Wallet: {{ message.WalletAddress }}</small>

      <ul v-if="message.Replies && message.Replies.length" class="list-group mt-3 ms-4">
        <CommunityMessageItem
          v-for="reply in message.Replies"
          :key="reply.id"
          :message="reply"
        />
      </ul>
    </li>
  `,
  components: {},
};

// Aby komponent rekurencyjny działał, trzeba mu przypisać siebie samego:
CommunityMessageItem.components.CommunityMessageItem = CommunityMessageItem;

// Stan komponentu
const messages = ref([
  {
    id: "1",
    Title: "Welcome",
    Message: "This is the first message",
    WalletAddress: "wallet123",
    Replies: [
      {
        id: "1-1",
        Title: "Reply",
        Message: "Thanks for the info!",
        WalletAddress: "wallet456",
        Replies: [],
      },
    ],
  },
]);

const form = reactive({
  title: "",
  message: "",
  walletAddress: "",
});

const errors = reactive({});

function submitForm() {
  // Reset errorów
  Object.keys(errors).forEach((key) => delete errors[key]);

  if (!form.title.trim()) errors.title = "Title is required";
  if (!form.message.trim()) errors.message = "Message is required";
  if (!form.walletAddress.trim()) errors.walletAddress = "Wallet address is required";

  if (Object.keys(errors).length === 0) {
    const newMessage = {
      id: crypto.randomUUID(),
      Title: form.title,
      Message: form.message,
      WalletAddress: form.walletAddress,
      Replies: [],
    };

    messages.value.push(newMessage);

    form.title = "";
    form.message = "";
    form.walletAddress = "";
  }
}
</script>

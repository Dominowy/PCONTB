<template>
  <div>
    <b-list-group vertical="md">
      <template v-if="content">
        <template v-for="user in content.users" :key="user.id">
          <b-list-group-item>
            <div class="d-flex justify-content-between">
              <div class="d-flex align-items-center">
                {{ user.email }}
              </div>
              <div>
                <button
                  class="btn btn-link text-secondary p-0"
                  @click="handleShowUpdateModal(user.id)"
                >
                  <i-material-symbols-settings-rounded style="font-size: 1.5rem" />
                </button>
                <button
                  class="btn btn-link text-secondary p-0"
                  @click="handleShowDeleteModal(user.id)"
                >
                  <i-material-symbols-person-remove style="font-size: 1.5rem" />
                </button>
                <button class="btn btn-link text-secondary p-0" @click="goToProfile(user.id)">
                  <i-material-symbols-article-person style="font-size: 1.5rem" />
                </button>
              </div>
            </div>
          </b-list-group-item>
        </template>
      </template>
      <b-list-group-item class="d-flex justify-content-end">
        <button class="btn btn-link text-secondary p-0" @click="handleShowAddModal">
          <i-material-symbols-person-add style="font-size: 1.5rem" />
        </button>
      </b-list-group-item>
    </b-list-group>
  </div>
</template>

<script setup>
import { useDisplay } from "@/composables/useDisplay";
import ApiClient from "@/services/ApiClient";
import { onMounted } from "vue";
import { useRouter } from "vue-router";

const router = useRouter();

onMounted(() => {
  loadData(onDataLoaded);
});

const onDataLoaded = async () => {
  return await ApiClient.request("account/users/get-all", {});
};

const goToProfile = async (id) => {
  router.push({
    name: "account:users:profile",
    params: { id },
  });
};

const { content, loadData } = useDisplay("Admin");
</script>

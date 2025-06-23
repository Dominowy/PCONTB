<template>
  <div>
    <b-list-group vertical="md">
      <template v-if="content">
        <template v-for="category in content.categories" :key="category.id">
          <b-list-group-item>
            <div class="d-flex justify-content-between">
              <div class="d-flex align-items-center">
                {{ category.name }}
              </div>
              <div>
                <button
                  class="btn btn-link text-secondary p-0"
                  @click="handleShowUpdateModal(category.id)"
                >
                  <i-material-symbols-settings-rounded style="font-size: 1.5rem" />
                </button>
                <button
                  class="btn btn-link text-secondary p-0"
                  @click="handleShowDeleteModal(category.id)"
                >
                  <i-material-symbols-person-remove style="font-size: 1.5rem" />
                </button>
                <button class="btn btn-link text-secondary p-0" @click="goToProfile(category.id)">
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

onMounted(() => {
  loadData(onDataLoaded);
});

const onDataLoaded = async () => {
  return await ApiClient.request("projects/categories/get-all", {});
};

const goToProfile = async (id) => {};

const { content, loadData } = useDisplay();
</script>

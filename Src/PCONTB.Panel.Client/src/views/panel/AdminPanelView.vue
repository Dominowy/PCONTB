<template>
  <div>
    <base-header title="Admin - Panel" />
    <b-row>
      <div>
        <b-card no-body>
          <b-tabs card>
            <b-tab title="Users">
              <panel-users-table />
            </b-tab>
            <b-tab title="Categories">
              <panel-categories-table />
            </b-tab>
            <b-tab title="Countries">
              <panel-countries-table />
            </b-tab>
          </b-tabs>
        </b-card>
      </div>
    </b-row>
  </div>
</template>

<script setup>
import PanelUsersTable from "./users/components/UsersTable.vue";
import PanelCategoriesTable from "./categories/components/CategoriesTable.vue";
import PanelCountriesTable from "./countries/components/CountriesTable.vue";

import { useView } from "@/composables/useView";

useView("Admin - Panel");
</script>

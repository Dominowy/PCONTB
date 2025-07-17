<template>
  <div>
    <b-card v-for="(content, index) in contents" :key="content.id">
      <div class="mb-2">
        <label :for="'types' + index" class="form-label d-block"> Type: </label>
        <select :id="'types' + index" class="form-select" v-model="content.type">
          <option v-for="type in types" :key="type.id" :value="type.value">
            {{ toPascalCase(type.value) }}
          </option>
        </select>
      </div>
      <div>
        <template v-if="content.type == 'title'">
          <base-form-input
            :id="'title' + index"
            class="mt-2"
            v-model="content.data.title"
            label="Title"
            placeholder="Enter title"
          />
        </template>
        <template v-if="content.type == 'subtitle'">
          <base-form-input
            :id="'subtitle' + index"
            class="mt-2"
            v-model="content.data.subtitle"
            label="Subtitle"
            placeholder="Enter subtitle"
          />
        </template>
        <template v-if="content.type == 'paragraph'">
          <textarea
            v-model="content.data.paragraph"
            class="form-control"
            rows="4"
            placeholder="Enter paragraph"
          ></textarea>
        </template>
        <template v-if="content.type == 'image'">
          <base-form-file
            :id="'file-image' + index"
            class="mt-2"
            property="Image"
            v-model="content.data.image"
            uploadUrl="/api/multimedia/upload-file"
            placeholder="Select image"
            :src="content.data.imageData"
          />
        </template>
        <template v-if="content.type == 'quote'">
          <base-form-input
            :id="'quote' + index"
            class="mt-2"
            v-model="content.data.quote"
            label="Quote"
            placeholder="Enter quote"
          />
        </template>
        <template v-if="content.type == 'video'">
          <base-form-file
            :id="'file-video' + index"
            class="mt-2"
            property="Video"
            v-model="content.data.video"
            uploadUrl="/api/multimedia/upload-file"
            placeholder="Select image"
            :src="content.data.videoData"
          />
        </template>
        <template v-if="content.type == 'button'"> Button</template>
        <template v-if="content.type == 'divider'"></template>
        <template v-if="content.type == 'list'"> List</template>
      </div>
    </b-card>
    <b-card>
      <div class="mb-2">
        <label for="types" class="form-label d-block"> Type: </label>
        <select id="types" class="form-select" @input="handleSelectType">
          <option value="">Select type</option>
          <option v-for="type in types" :key="type.id" :value="type.value">
            {{ toPascalCase(type.value) }}
          </option>
        </select>
      </div>
      <div class="d-flex mb-2">
        <template v-if="selectType == 'title'"> Title </template>
        <template v-if="selectType == 'subtitle'"> Subititle </template>
        <template v-if="selectType == 'paragraph'"> Paragraph </template>
        <template v-if="selectType == 'image'"> Image</template>
        <template v-if="selectType == 'quote'"> Quote</template>
        <template v-if="selectType == 'video'"> Video</template>
        <template v-if="selectType == 'button'"> Button</template>
        <template v-if="selectType == 'divider'"> Divider</template>
        <template v-if="selectType == 'list'"> List</template>
      </div>
      <div class="d-flex justify-content-end">
        <b-button variant="primary" @click="addContent"> Add </b-button>
      </div>
    </b-card>
  </div>
</template>

<script setup>
import { ref } from "vue";
import { v4 as uuidv4 } from "uuid";

defineProps({
  types: Array,
});

const contents = ref([]);
const selectType = ref(null);

const addContent = () => {
  if (!selectType.value) return;

  contents.value.push({
    id: uuidv4(),
    type: selectType.value,
    order: 1,
    data: {},
  });

  selectType.value = "";
};

const handleSelectType = (e) => {
  selectType.value = e.target.value;
};

function toPascalCase(str) {
  if (!str) return "";
  return str[0].toUpperCase() + str.slice(1);
}
</script>

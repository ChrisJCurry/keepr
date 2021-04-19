<template>
  <div class="home container-fluid">
    <div class="card-columns mt-3">
      <div v-for="k in state.keeps" :key="k.id">
        <Keep :keep-prop="k" />
        <KeepModal :keep-prop="k" />
      </div>
    </div>
  </div>
</template>

<script>
import { reactive, computed, onMounted } from 'vue'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
export default {
  name: 'Home',
  setup() {
    const state = reactive({
      keeps: computed(() => AppState.keeps)
    })
    onMounted(async() => {
      await keepsService.GetAll()
    })

    return {
      state
    }
  }
}
</script>

<style scoped lang="scss">

.card-columns {
  //small
  @media(max-width: 767px) {
    column-count: 2;
  }
  @media(min-width: 768px) {
    column-count: 3;
  }
  @media(min-width: 1200px) {
    column-count: 4;
  }
}
</style>

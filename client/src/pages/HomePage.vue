<template>
  <div class="home container-fluid">
    <div class="card-columns mt-3">
      <div v-for="k in state.keeps" :key="k.id" class="m-3">
        <Keep :keep-prop="k" />
        <KeepModal />
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
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults),
      account: computed(() => AppState.account)
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
  @media(max-width: 599px) {
    column-count: 1;
  }
  //small
  @media(min-width: 600px) {
    column-count: 2;
  }
  @media(min-width: 773px) {
    column-count: 3;
  }
  @media(min-width: 992px) {
    column-count: 3;
  }
  @media(min-width: 1920px) {
    column-count: 4;
  }
}
</style>

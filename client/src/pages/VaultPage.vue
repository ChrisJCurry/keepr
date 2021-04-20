<template>
  <div class="vault-page container-fluid text-center">
    <h1>Welcome {{ state.account.nickName }}</h1>
    <div class="row" v-if="state.keeps">
      <div v-for="k in state.keeps" :key="k.id" class="col-3 mx-2">
        <Keep :keep-prop="k" />
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import { useRoute } from 'vue-router'
export default {
  name: 'Vault',
  setup() {
    const route = useRoute()
    const state = reactive({
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.keeps)
    })
    onMounted(async() => {
      await keepsService.getByVaultId(route.params.id)
    })
    return {
      state
    }
  }
}
</script>

<style scoped lang="scss">
img {
  max-width: 100px;
}

.icon {
  top: 33%;
}

.title {
  font-size: 4rem;
}

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

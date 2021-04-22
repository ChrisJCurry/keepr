<template>
  <div class="vault-page container-fluid text-center">
    <h1>Welcome {{ state.account.nickName }}</h1>
    <h1>Vault: {{ state.vault.name }}</h1>
    <div class="row" v-if="state.keeps">
      <div v-for="k in state.keeps" :key="k.id" class="col-3 mx-2">
        <Keep :keep-prop="k" />
        <KeepModal :vault-keep-id-prop="k.vaultKeepId" />
      </div>
      <div v-if="state.keeps.length < 1">
        <h1>This vault has no keeps.</h1>
        <button class="btn btn-primary" @click="goBack">
          Go back
        </button>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import { vaultsService } from '../services/VaultsService'
import Router from '../router'
import NotificationsService from '../services/NotificationsService'
export default {
  name: 'Vault',
  setup() {
    const state = reactive({
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.keeps),
      vault: computed(() => AppState.vault)
    })
    onMounted(async() => {
      if (state.account) {
        vaultsService.getById(Router.currentRoute.value.params.id)
        keepsService.getByVaultId(Router.currentRoute.value.params.id)
      }
      if (!state.account.name && state.vault.isPrivate) {
        await NotificationsService.genericError('Something went wrong getting keeps from the Vault.')
        Router.push({ name: 'Home' })
        keepsService.GetAll()
      }
    })
    return {
      state,
      goBack() {
        window.history.back()
      }
    }
  },
  watch: {
    'state.account': {
      handler(val, oldVal) {
        try {
          vaultsService.getById(Router.currentRoute.value.params.id)
          keepsService.getByVaultId(Router.currentRoute.value.params.id)
        } catch (err) {
          Router.push({ name: 'Home' })
        }
      },
      deep: true
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

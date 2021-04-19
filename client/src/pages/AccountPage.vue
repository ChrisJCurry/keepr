<template>
  <div class="account container-fluid text-center">
    <h1>Welcome {{ state.account.nickName }}</h1>
    <img class="rounded" :src="state.account.picture" alt="" />
    <p>{{ state.account.email }}</p>
    <div class="row text-left ml-3">
      <div class="col-4 col-md-3">
        <h1 class="title">
          Vaults
        </h1>
      </div>
      <div class="col-1 offset-3 offset-sm-0" data-toggle="modal" data-target="#new-vault-modal">
        <i class="gg-add-r mr-2 icon"></i>
      </div>
    </div>
    <div class="row" v-if="state.userVaults">
      <Vault v-for="v in state.userVaults" :key="v.id" :vault-prop="v" class="col-3 mx-2" />
    </div>
    <div class="row text-left ml-3">
      <div class="col-4 col-md-3">
        <h1 class="title">
          Keeps
        </h1>
      </div>
      <div class="col-1 offset-3 offset-sm-0" data-toggle="modal" data-target="#new-keep-modal">
        <i class="gg-add-r mr-2 icon"></i>
      </div>
    </div>
    <div class="row">
      <div v-if="state.userKeeps">
        <div class="card-columns">
          <div v-for="k in state.userKeeps" :key="k.id">
            <Keep :keep-prop="k" />
            <KeepModal :keep-prop="k" />
          </div>
        </div>
      </div>
      <button type="button" class="btn btn-primary font-weight-bold" data-toggle="modal" data-target="#new-vault-modal">
        Create Vault
      </button>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import { vaultsService } from '../services/VaultsService'
export default {
  name: 'Account',
  setup() {
    const state = reactive({
      account: computed(() => AppState.account),
      userKeeps: computed(() => AppState.userKeeps),
      userVaults: computed(() => AppState.userVaults)
    })
    onMounted(async() => {
      await keepsService.GetByProfile(state.account.id)
      await vaultsService.GetByProfile(state.account.id)
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

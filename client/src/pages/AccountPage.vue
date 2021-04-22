<template>
  <div class="account container-fluid">
    <div class="row my-5">
      <div class="col-5 col-sm-3 col-md-2">
        <img class="rounded mt-2" :src="state.account.picture" alt="" />
      </div>
      <div class="col-4 text-left">
        <p class="h1">
          {{ state.account.nickName }}
        </p>
        <p class="h4">
          Vaults: {{ state.userVaults.length }}
        </p>
        <p class="h4">
          Keeps: {{ state.userKeeps.length }}
        </p>
      </div>
    </div>
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
    <div class="row justify-content-center" v-if="state.userVaults">
      <div class="card-columns">
        <div v-for="v in state.userVaults" :key="v.id" class="col">
          <Vault :vault-prop="v" />
        </div>
      </div>
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
    <div class="row ml-3" v-if="state.userKeeps">
      <div class="card-columns">
        <div v-for="k in state.userKeeps" :key="k.id">
          <Keep :keep-prop="k" />
          <KeepModal />
        </div>
      </div>
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
      await keepsService.getAccountKeeps(state.account.id)
      await vaultsService.getAccountVaults(state.account.id)
    })
    return {
      state
    }
  }
}
</script>

<style scoped lang="scss">
img {
  max-width: 30rem;
}

.icon {
  top: 33%;
}

.title {
  font-size: 4rem;
}

.card-columns {
  //small
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

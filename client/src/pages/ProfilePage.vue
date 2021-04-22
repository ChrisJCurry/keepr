<template>
  <div class="profile container-fluid text-center">
    <div class="row my-5">
      <div class="col-5 col-sm-3 col-md-2">
        <img class="rounded mt-2" :src="state.profile.picture" alt="" />
      </div>
      <div class="col-4 text-left">
        <p class="h1">
          {{ state.profile.nickName }}
        </p>
        <p class="h4">
          Vaults: {{ state.vaults.length }}
        </p>
        <p class="h4">
          Keeps: {{ state.keeps.length }}
        </p>
      </div>
    </div>
    <div class="row text-left ml-3">
      <div class="col-4 col-md-2">
        <h1 class="title">
          Vaults
        </h1>
      </div>
    </div>
    <div class="row" v-if="state.vaults">
      <div v-for="v in state.vaults" :key="v.id" class="col-3 mx-2">
        <router-link :to="{ name: 'Vault', params: {id: v.id} }">
          <Vault :vault-prop="v" />
        </router-link>
      </div>
    </div>
    <div class="row text-left ml-3">
      <div class="col-4 col-md-2">
        <h1 class="title">
          Keeps
        </h1>
      </div>
    </div>
    <div class="row">
      <div v-if="state.keeps">
        <div class="card-columns">
          <div v-for="k in state.keeps" :key="k.id">
            <Keep :keep-prop="k" />
            <KeepModal />
          </div>
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
import { profilesService } from '../services/ProfilesService'
import { useRoute } from 'vue-router'

export default {
  name: 'Profile',
  setup() {
    const route = useRoute()
    const state = reactive({
      account: computed(() => AppState.account),
      profile: {},
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults)
    })
    onMounted(async() => {
      state.profile = await profilesService.getProfile(route.params.id)
      await keepsService.getByProfile(state.profile.id)
      await vaultsService.getByProfile(state.profile.id)
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

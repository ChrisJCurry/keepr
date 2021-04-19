<template>
  <div class="profile container-fluid text-center">
    <h1>Welcome to {{ state.profile.nickName }}'s Profile!</h1>
    <img class="rounded" :src="state.profile.picture" alt="" />
    <div class="row text-left ml-3">
      <div class="col-4 col-md-2">
        <h1 class="title">
          Vaults
        </h1>
      </div>
    </div>
    <div class="row" v-if="state.vaults">
      <Vault v-for="v in state.vaults" :key="v.id" :vault-prop="v" class="col-3 mx-2" />
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
import { logger } from '../utils/Logger'
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
      logger.log(route.params.id)
      state.profile = await profilesService.getProfile(route.params.id)
      logger.log('profile: ', state.profile)
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

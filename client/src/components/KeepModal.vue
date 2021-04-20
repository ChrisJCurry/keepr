<template>
  <div class="modal fade keep-modal"
       tabindex="-1"
       id="keep-modal"
       role="dialog"
       aria-labelledby="myLargeModalLabel"
       aria-hidden="true"
  >
    <div class="modal-dialog modal-dialog-centered modal-xl modal-size" role="document">
      <div class="modal-content">
        <div class="modal-body m-1">
          <div class="row d-flex">
            <div class="col-md-6">
              <img :src="state.keep.img" />
            </div>
            <div class="col-md-6 pl-0 ml-0 mt-2">
              <div class="row d-none d-md-flex">
                <div class="col-3 offset-2">
                  <span class="d-flex">
                    <i class="gg-eye mr-2"></i>
                    <p>{{ state.keep.views }}</p>
                  </span>
                </div>
                <div class="col-3">
                  <span class="d-flex">
                    <i class="gg-bookmark mr-3"></i>
                    <p>0</p>
                  </span>
                </div>
                <div class="col-3">
                  <span class="d-flex">
                    <i class="gg-share mr-4 mt-2"></i>
                    <p>0</p>
                  </span>
                </div>
              </div>
              <div class="row text-center mt-3">
                <div class="col">
                  <h2>
                    {{ state.keep.name }}
                  </h2>
                </div>
              </div>
              <div class="row text-center keep-description">
                <div class="col-12">
                  <h2>
                    {{ state.keep.description }}
                  </h2>
                </div>
              </div>
              <div class="keep-description-border d-none d-md-flex"></div>
              <div class="row keep-footer text-center p-0 m-0">
                <div class="col-12 col-sm-6">
                  <div class="dropdown">
                    <button class="btn btn-secondary dropdown-toggle"
                            type="button"
                            id="dropdownMenuButton"
                            data-toggle="dropdown"
                            aria-haspopup="true"
                            aria-expanded="false"
                    >
                      Add to Vault
                    </button>
                    <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                      <div v-for="v in state.userVaults" :key="v.id" @click="selectVault(v.creatorId)">
                        <a class="dropdown-item" @click="addToVault(v.id)">{{ v.name }}</a>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="col-12 col-sm-6 mt-5 mb-2 mt-md-0 mb-md-0">
                  <div class="row justify-content-center justify-content-md-end align-items-md-end mr-md-5" data-dismiss="modal" v-if="state.keep.creator" @click="viewProfile(state.keep.creatorId)">
                    <div class="col-2 mr-5">
                      {{ state.keep.creator.nickName }}
                    </div>
                    <div class="col-1 ml-sm-2">
                      <img :src="state.keep.creator.picture" class="keep-profile">
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { useRouter } from 'vue-router'
import { keepsService } from '../services/KeepsService'

export default {

  name: 'KeepModal',
  setup() {
    const router = useRouter()
    const state = reactive({
      keep: computed(() => AppState.keep),
      userVaults: computed(() => AppState.userVaults)
    })
    onMounted(() => {
      logger.log(state.userVaults)
    })
    return {
      state,
      async selectVault(id) {
        logger.log(id)
      },
      async viewProfile(id) {
        router.push({ name: 'Profile', params: { id: id } })
      },
      async addToVault(vaultId) {
        await keepsService.addToVault(state.keep, vaultId)
      }
    }
  }
}
</script>

<style scoped lang="scss">
i {
  position: relative;
  top: 1px;
  left: 0px;
}

.keep-profile {
  height: 2rem;
  width: 2rem;
}

.modal-size {
  max-width: 95%;
}

.keep-footer {
  @media(max-width: 768px) {
    bottom: 1%;
    width: 100%;
  }

  @media(min-width: 769px) {
    position: absolute;
    bottom: 1%;
    width: 100%;
  }
}

.keep-description {
  @media(max-width: 768px) {
    margin-bottom: 8rem;
  }
  @media(min-width: 769px) {
    margin-top: 4rem;
  }
}
.keep-description-border {
  position: absolute;
  margin-top: 1rem;
  left: 25%;
  width: 50%;
  border-bottom: 1px solid black;
}
.modal-body {
  padding: 0 !important;
}
.modal-dialog {
  padding: 0 !important;
}

.modal-content {
  border-radius: 2% !important;
}

img {
  width: 100%;
  /* border-top-left-radius: 3%;
  border-bottom-left-radius: 3%; */
  border-radius: 2%;
}
</style>

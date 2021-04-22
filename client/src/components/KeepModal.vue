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
        <button type="button" class="close top-right" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
        <div class="modal-body m-1">
          <div class="row d-flex">
            <div class="col-12 col-lg-6">
              <img :src="state.keep.img" />
            </div>
            <div class="col-12 col-lg-6 pl-0 ml-0 mt-2">
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
                    <p>{{ state.keep.keeps }}</p>
                  </span>
                </div>
                <div class="col-3">
                  <span class="d-flex">
                    <i class="gg-share mr-4 mt-2"></i>
                    <p>{{ state.keep.shares }}</p>
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
                    <div class="keep-description-border d-none d-xl-flex"></div>
                  </h2>
                </div>
              </div>
              <div class="row text-center mx-0 pb-4" v-if="state.account.name">
                <div class="col" v-if="vaultKeepIdProp != -1">
                  <button class="btn btn-danger p-1" type="button" @click="removeFromVault(vaultKeepIdProp)">
                    Remove from Vault
                  </button>
                </div>
                <div class="col">
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
                      <div v-for="v in state.userVaults" :key="v.id">
                        <a class="dropdown-item" @click="addToVault(v.id)">{{ v.name }}</a>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <div class="row keep-footer">
                <div class="col">
                  <div class="closeButton" @click="deleteKeep" v-if="state.keep.creatorId == state.account.id">
                    <i class="gg-trash"></i>
                  </div>
                </div>
                <div class="col">
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
import { computed, reactive } from 'vue'
import { AppState } from '../AppState'
import { useRoute, useRouter } from 'vue-router'
import { keepsService } from '../services/KeepsService'
import NotificationsService from '../services/NotificationsService'

export default {

  name: 'KeepModal',
  props: {
    vaultKeepIdProp: { type: Number, default: -1 }
  },
  setup(props) {
    const router = useRouter()
    const route = useRoute()
    const state = reactive({
      account: computed(() => AppState.account),
      profile: computed(() => AppState.profile),
      keep: computed(() => AppState.keep),
      vault: computed(() => AppState.vault),

      userVaults: computed(() => AppState.userVaults)
    })
    return {
      state,
      async viewProfile(id) {
        if (id === state.account.id) {
          router.push({ name: 'Account' })
        } else {
          router.push({ name: 'Profile', params: { id: id } })
        }
      },
      async addToVault(vaultId) {
        await keepsService.addToVault(state.keep, vaultId)
      },
      async removeFromVault(vkId) {
        await keepsService.removeFromVault(vkId)
        // eslint-disable-next-line no-undef
        $('#keep-modal').modal('hide')
        // eslint-disable-next-line no-undef
        $('.modal-backdrop').hide()
        if (route.params.id === state.vault.id.toString()) {
          await keepsService.getByVaultId(state.vault.id)
        } else {
          await NotificationsService.genericError()
          router.push({ name: 'Home' })
        }
      },
      async deleteKeep(id) {
        if (await keepsService.delete(state.keep)) {
          // eslint-disable-next-line no-undef
          $('#keep-modal').modal('hide')
          // eslint-disable-next-line no-undef
          $('.modal-backdrop').hide()
          if (route.path === '/account') {
            await keepsService.getAccountKeeps(state.account.id)
          } else if (route.path === '/') {
            const index = AppState.keeps.findIndex(k => k.id === state.keep.id)
            AppState.keeps.splice(index, 1)
          } else if (route.path === '/vaults/' + AppState.vault.id) {
            await keepsService.getByVaultId(AppState.vault.id)
          } else {
            await NotificationsService.genericError()
            router.push({ name: 'Home' })
          }
        }
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
  @media(max-width: 767px) {
    bottom: 1%;
    width: 100%;
  }

  @media(min-width: 768px) {
    position: absolute;
    bottom: 1%;
    width: 100%;
  }
}
.closeButton {
  position: absolute;
  bottom: 15%;
  right: 50%;
  color: red;
}

.top-right {
  position: absolute;
  z-index: 2;
  top: 2%;
  right: 2%;
}

.keep-description {
  @media(max-width: 768px) {
    margin-bottom: 6rem;
  }
  @media(min-width: 769px) {
    margin-top: .5rem;
    margin-bottom: 3rem;
  }
  @media(min-width: 1200px) {
    margin-top: 3rem;
  }
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

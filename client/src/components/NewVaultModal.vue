<template>
  <div class="modal fade"
       id="new-vault-modal"
       tabindex="-1"
       role="dialog"
       aria-labelledby="exampleModalCenterTitle"
       aria-hidden="true"
  >
    <div class="modal-dialog modal-dialog-centered" role="document">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="exampleModalCenterTitle">
            New Vault
          </h5>
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <div class="modal-body">
          <div class="row mt-2 justify-content-center">
            <input type="text" placeholder="Title" v-model="state.newVault.name" />
          </div>
          <div class="row my-2 justify-content-center">
            <input type="text" placeholder="Description" v-model="state.newVault.description" />
          </div>
          <div class="row justify-content-center text-center">
            <div class="col-12">
              <label for="vault-private">Private?</label>
            </div>
          </div>
          <div class="col-12 p-0 justify-content-center text-center">
            <input type="checkbox" id="vault-private" v-model="state.newVault.isPrivate" />
            <p class="label-muted">
              Check this if you don't want it visible to anyone else.
            </p>
          </div>
        </div>
        <div class="modal-footer justify-content-between">
          <button type="button" class="btn btn-secondary" data-dismiss="modal">
            Close
          </button>
          <button type="button" class="btn btn-primary" data-dismiss="modal" @click="createVault">
            Create Vault
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { reactive } from 'vue'
import { logger } from '../utils/Logger'
import { vaultsService } from '../services/VaultsService'
export default {
  name: 'NewVaultModal',
  setup() {
    const state = reactive({
      newVault: {}
    })
    return {
      state,
      async createVault() {
        try {
          await vaultsService.create(state.newVault)
          await vaultsService.getAccountVaults()
          state.newVault = {}
        } catch (err) {
          logger.log(err)
        }
      }
    }
  }
}
</script>

<style scoped>
.label-muted {
  font-size: .7rem;
  color: rgba(0, 0, 0, 0.7);
}
</style>

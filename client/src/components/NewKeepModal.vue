<template>
  <div class="modal fade"
       id="new-keep-modal"
       tabindex="-1"
       role="dialog"
       aria-labelledby="exampleModalCenterTitle"
       aria-hidden="true"
  >
    <div class="modal-dialog modal-dialog-centered" role="document">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="exampleModalCenterTitle">
            New Keep
          </h5>
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <div class="modal-body">
          <div class="row mt-2">
            <input type="text" placeholder="Title" v-model="state.newKeep.name" />
          </div>
          <div class="row my-2">
            <input type="text" placeholder="Description" v-model="state.newKeep.description" />
          </div>
          <div class="row my-2">
            <input type="text" placeholder="Image URL?" v-model="state.newKeep.img" />
          </div>
          <div class="row my-2">
            <input type="file" placeholder="Image File" @change="onChange" accept="image/*" />
          </div>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-dismiss="modal">
            Close
          </button>
          <button type="button" class="btn btn-primary" data-dismiss="modal" @click="createKeep">
            Create Keep
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, reactive } from 'vue'
import { logger } from '../utils/Logger'
import { keepsService } from '../services/KeepsService'
import { useRoute } from 'vue-router'
import { AppState } from '../AppState'
export default {
  name: 'NewKeepModal',
  setup() {
    const route = useRoute()
    const state = reactive({
      newKeep: {},
      account: computed(() => AppState.account),
      file: 'https://i.ibb.co/cwq36B9/Choko-Scrunch.jpg'
    })
    return {
      state,
      async createKeep() {
        try {
          logger.log(state.newKeep)
          state.newKeep.img = state.file
          await keepsService.create(state.newKeep)
          logger.log(route.path)
          if (route.path === '/account') {
            await keepsService.getByProfile(state.account.id)
          }
          state.newKeep = {}
        } catch (err) {
          logger.log(err)
        }
      },
      onChange() {
        const files = event.target.files
        const fileArray = Array.from(files)
        logger.log(fileArray)
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

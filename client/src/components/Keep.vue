<template>
  <div class="card">
    <img class="card-img-top"
         :src="keepProp.img"
         alt="hi"
         @click="getKeep"
         data-toggle="modal"
         :data-target="'#keep-modal'"
    >
    <div class="keep-overlay h2 text-white row ml-0">
      <div class="col-10 keep-title">
        {{ keepProp.name }}
      </div>
      <div class="col-2 keep-creator">
        <router-link :to="{ name: 'Profile', params: {id: keepProp.creatorId} }">
          <img :src="keepProp.creator.picture" class="keep-profile">
        </router-link>
      </div>
    </div>
  </div>
</template>

<script>
import { AppState } from '../AppState'
import { reactive, computed } from 'vue'
import { keepsService } from '../services/KeepsService'

export default {
  name: 'Keep',
  props: {
    keepProp: {
      type: Object, required: true
    }
  },
  setup(props) {
    const state = reactive({
      keeps: computed(() => AppState.keeps),
      user: computed(() => AppState.user),
      keep: computed(() => AppState.keep)
    })
    return {
      state,
      async getKeep() {
        await keepsService.getById(props.keepProp.id)
      }
    }
  }
}
</script>

<style scoped>
  .keep-overlay {
    position: absolute;
    border-radius: 3%;
    top: 90%;
    left: 0%;
    width: 100%;
    height: 10%;
    background: linear-gradient(to bottom, transparent 0%, black 90%);
  }

  .card {
    border-radius: 3%;
  }

  .keep-title {
    position: absolute;
    bottom: 1%;
    left: 2%;
    font-size: 1.75rem;
  }

  .keep-creator {
    position: absolute;
    bottom: 1%;
    right: 3%;
    font-size: 1rem;
  }

  .keep-profile {
  height: 2rem;
  width: 2rem;
}

  @media(max-width:575px) {
    .keep-title {
      font-size: .75rem;
    }
  }

  @media(max-width:768px) {
    .keep-title {
      font-size: 1.05rem;
    }
  }

  .card-img-top {
    border-radius: 3%;
  }

</style>

<template>
  <div class="card card-shadow">
    <img class="card-img-top"
         :src="keepProp.img"
         alt="hi"
         @click="getKeep"
         data-toggle="modal"
         :data-target="'#keep-modal'"
    >
    <div class="keep-overlay h2 text-white row ml-0" @click="viewProfile(keepProp.creatorId)">
      <div class="col-10 keep-title">
        {{ keepProp.name }}
      </div>
      <div class="col-2 keep-creator" v-if="keepProp.creator">
        <img :src="keepProp.creator.picture" class="keep-profile">
      </div>
    </div>
  </div>
</template>

<script>
import { AppState } from '../AppState'
import { reactive, computed } from 'vue'
import { keepsService } from '../services/KeepsService'
import { useRouter } from 'vue-router'

export default {
  name: 'Keep',
  props: {
    keepProp: {
      type: Object, required: true
    }
  },
  setup(props) {
    const router = useRouter()
    const state = reactive({
      keeps: computed(() => AppState.keeps),
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      keep: computed(() => AppState.keep)
    })
    return {
      state,
      async getKeep() {
        await keepsService.getById(props.keepProp.id)
      },
      async viewProfile(id) {
        if (id === state.account.id) {
          router.push({ name: 'Account' })
        } else {
          router.push({ name: 'Profile', params: { id: id } })
        }
      }
    }
  }
}
</script>

<style scoped lang="scss">
  .keep-overlay {
    position: absolute;
    border-radius: 15%;
    top: 85%;
    left: 0;
    width: 100%;
    height: 15%;
    background: linear-gradient(to bottom, transparent 0%, transparent 20%, black 70%);
  }

  .card {
    border-radius: 3%;
  }

  .card-shadow {
    box-shadow: 10px 10px 5px grey;
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

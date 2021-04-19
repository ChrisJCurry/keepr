<template>
  <div class="card">
    <div @click="expand" data-toggle="modal" :data-target="'#keep-modal-' + keepProp.id">
      <img class="card-img-top" :src="keepProp.img" alt="hi">
      <div class="keep-overlay h2 text-white">
        <div class="keep-title">
          {{ keepProp.name }}
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { AppState } from '../AppState'
import { reactive, computed } from 'vue'
import { logger } from '../utils/Logger'

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
      user: computed(() => AppState.user)
    })
    return {
      state,
      expand() {
        logger.log(props.keepProp.id)
      }
    }
  }
}
</script>

<style scoped>
  .keep-overlay {
    position: absolute;
    border-radius: 3%;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background: linear-gradient(to bottom, transparent 0%, transparent 80%, black 90%);
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

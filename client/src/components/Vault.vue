<template>
  <div class="card">
    <div @click="expand">
      <img class="card-img-top" src="https://i.ibb.co/cwq36B9/Choko-Scrunch.jpg" alt="hi">

      <div class="vault-overlay h2 text-white">
        <div class="vault-title">
          {{ vaultProp.name }}
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
  name: 'Vault',
  props: {
    vaultProp: {
      type: Object, required: true
    }
  },
  setup(props) {
    const state = reactive({
      vaults: computed(() => AppState.vaults),
      user: computed(() => AppState.user)
    })
    return {
      state,
      expand() {
        logger.log(props.vaultProp.id)
      }
    }
  }
}
</script>

<style scoped>
  .vault-overlay {
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

  .vault-title {
    position: absolute;
    bottom: 1%;
    left: 2%;
    font-size: 1.75rem;
  }

  @media(max-width:575px) {
    .vault-title {
      font-size: .75rem;
    }
  }

  @media(max-width:768px) {
    .vault-title {
      font-size: 1.05rem;
    }
  }

  .card-img-top {
    border-radius: 3%;
  }

</style>

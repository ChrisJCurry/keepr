<template>
  <div class="card">
    <div>
      <div v-if="state.account">
        <div class="closeButton" v-if="state.account.id === vaultProp.creatorId">
          <i class="gg-trash" @click="deleteVault(vaultProp.id)" />
        </div>
      </div>
      <router-link :to="{ name: 'Vault', params: {id: vaultProp.id} }">
        <img class="card-img-top" src="https://i.ibb.co/cwq36B9/Choko-Scrunch.jpg" alt="hi">
      </router-link>
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
import { vaultsService } from '../services/VaultsService'

export default {
  name: 'Vault',
  props: {
    vaultProp: {
      type: Object, required: true
    }
  },
  setup(props) {
    const state = reactive({
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.vaults),
      user: computed(() => AppState.user)
    })
    return {
      state,
      async deleteVault(id) {
        await vaultsService.delete(id)
        const index = AppState.userVaults.findIndex(v => v.id === id)
        AppState.userVaults.splice(index, 1)
      }
    }
  }
}
</script>

<style scoped>
  .vault-overlay {
    position: absolute;
    border-radius: 3%;
    top: 85%;
    left: 0;
    width: 100%;
    height: 15%;
    background: linear-gradient(to bottom, transparent 0%, transparent 20%, black 70%);
  }

  .closeButton {
  position: absolute;
  top: 5%;
  right: 5%;
  color: red;
  z-index: 10;
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

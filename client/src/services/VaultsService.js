import { AppState } from '../AppState'
import { api } from './AxiosService'
import NotificationsService from '../services/NotificationsService'
import Router from '../router'

class VaultsService {
  async getAccountVaults() {
    try {
      const res = await api.get('account/vaults')
      AppState.userVaults = res.data
    } catch (err) {
      await NotificationsService.genericError()
    }
  }

  async getByProfile(id) {
    try {
      const res = await api.get('api/profiles/' + id + '/vaults')
      AppState.vaults = res.data
    } catch (err) {
      await NotificationsService.genericError()
    }
  }

  async getById(id) {
    try {
      const res = await api.get('api/vaults/' + id)
      AppState.vault = res.data
    } catch (err) {
      await NotificationsService.genericError()
      Router.push({ name: 'Home' })
    }
  }

  async create(vaultData) {
    try {
      const res = await api.post('api/vaults', vaultData)
      AppState.vaults.push(res.data)
      await NotificationsService.success()
    } catch (err) {
      await NotificationsService.genericError('Unable to create the Vault')
    }
  }

  async delete(vaultId) {
    try {
      if (await NotificationsService.confirmAction('Vault')) {
        await api.delete('api/vaults/' + vaultId)
      }
    } catch (err) {
      await NotificationsService.genericError('Something went wrong deleting your Vault.')
    }
  }
}

export const vaultsService = new VaultsService()

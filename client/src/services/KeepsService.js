import { AppState } from '../AppState'
import { api } from './AxiosService'
import NotificationsService from '../services/NotificationsService'
import Router from '../router'
class KeepsService {
  async GetAll() {
    try {
      const res = await api.get('api/keeps')
      AppState.keeps = res.data
    } catch (err) {
      await NotificationsService.genericError()
    }
  }

  async getAccountKeeps(id) {
    try {
      const res = await api.get('account/keeps')
      AppState.userKeeps = res.data
    } catch (err) {
      await NotificationsService.genericError()
    }
  }

  async getByProfile(id) {
    try {
      const res = await api.get('api/profiles/' + id + '/keeps')
      AppState.keeps = res.data
    } catch (err) {
      await NotificationsService.genericError()
    }
  }

  async getById(id) {
    try {
      const res = await api.get('api/keeps/' + id)
      AppState.keep = res.data
    } catch (err) {
      await NotificationsService.genericError()
    }
  }

  async create(keepData) {
    try {
      const res = await api.post('api/keeps', keepData)
      AppState.userKeeps.push(res.data)
      AppState.keeps.push(res.data)
    } catch (err) {
      await NotificationsService.genericError('Unable to create this Keep.')
    }
  }

  async delete(keepData) {
    try {
      if (await NotificationsService.confirmAction('Keep')) {
        await api.delete('api/keeps/' + keepData.id)
        return true
      }
      return false
    } catch (err) {
      await NotificationsService.genericError('Something went wrong deleting your Keep.')
    }
  }

  async addToVault(keep, vaultId) {
    try {
      const ids = {
        keepId: keep.id,
        vaultId: vaultId
      }
      await api.post('api/vaultkeeps/', ids)
      await NotificationsService.success()
    } catch (err) {
      await NotificationsService.genericError("You've already added this Keep to this Vault.")
    }
  }

  async removeFromVault(vkId) {
    try {
      await api.delete('api/vaultkeeps/' + vkId)
      if (await NotificationsService.confirmRemove('Keep')) {
        await NotificationsService.success()
        return true
      }
      return false
    } catch (err) {
      await NotificationsService.genericError('Something went wrong removing this from the Vault.')
    }
  }

  async getByVaultId(vaultId) {
    try {
      const res = await api.get('api/vaults/' + vaultId + '/keeps')
      AppState.keeps = res.data
    } catch (err) {
      await NotificationsService.genericError('Unable to get from this Vault.')
      Router.push({ name: 'Home' })
    }
  }
}

export const keepsService = new KeepsService()

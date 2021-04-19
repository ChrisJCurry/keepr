import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class VaultsService {
  async getAccountVaults() {
    try {
      const res = await api.get('account/vaults')
      logger.log(res)
      AppState.userVaults = res.data
    } catch (err) {
      logger.log(err)
    }
  }

  async getByProfile(id) {
    try {
      const res = await api.get('api/profiles/' + id + '/vaults')
      logger.log(res)
      AppState.vaults = res.data
    } catch (err) {
      logger.log(err)
    }
  }

  async create(vaultData) {
    try {
      const res = await api.post('api/vaults', vaultData)
      AppState.vaults.push(res.data)
      logger.log(res)
    } catch (err) {
      logger.log(err)
    }
  }
}

export const vaultsService = new VaultsService()

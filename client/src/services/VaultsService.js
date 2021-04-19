import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class VaultsService {
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
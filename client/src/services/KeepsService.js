import { AppState } from '../AppState'
import { api } from './AxiosService'
import { logger } from '../utils/Logger'

class KeepsService {
  async GetAll() {
    try {
      const res = await api.get('api/keeps')
      logger.log(res)
      AppState.keeps = res.data
    } catch (err) {
      logger.log(err)
    }
  }

  async GetByProfile(id) {
    try {
      const res = await api.get('account/keeps')
      logger.log(res)
      AppState.userKeeps = res.data
    } catch (err) {
      logger.log(err)
    }
  }

  async create(keepData) {
    try {
      const res = await api.post('api/keeps', keepData)
      AppState.keeps.push(res.data)
      logger.log(res)
    } catch (err) {
      logger.log(err)
    }
  }
}

export const keepsService = new KeepsService()

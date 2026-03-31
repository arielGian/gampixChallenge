import axios from 'axios'

const API_BASE_URL = `${import.meta.env.VITE_API_URL}`

/**
 * Creates a new bet via POST /api/bets
 * @param {{ userId: number, game: string, stake: number, winAmount: number }} data
 */
export const createBet = async (data) => {
  const response = await axios.post(`${API_BASE_URL}/api/bets`, data)
  return response.data
}

/**
 * Fetches global stats via GET /stats
 */
export const getStats = async () => {
  const response = await axios.get(`${API_BASE_URL}/stats`)
  return response.data
}

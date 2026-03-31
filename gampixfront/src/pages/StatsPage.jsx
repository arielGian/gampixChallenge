import { useQuery } from '@tanstack/react-query'
import { getStats } from '../api/betsApi'
import styles from './StatsPage.module.css'

const StatsPage = () => {
  const { data, isLoading, isError, isFetching, refetch } = useQuery({
    queryKey: ['stats'],
    queryFn: getStats,
  })

  return (
    <div className={styles.wrapper}>
      <div className={styles.container}>
        <div className={styles.header}>
          <h1 className={styles.title}>Estadísticas</h1>
          <button
            className={styles.refreshButton}
            onClick={() => refetch()}
            disabled={isFetching}
          >
            {isFetching ? 'Actualizando...' : 'Actualizar'}
          </button>
        </div>

        {isLoading && (
          <div className={`${styles.status} ${styles.statusLoading}`}>
            Cargando...
          </div>
        )}

        {isError && (
          <div className={`${styles.status} ${styles.statusError}`}>
            Error al cargar estadísticas
          </div>
        )}

        {data && (
          <>
            <div className={styles.section}>
              <p className={styles.sectionTitle}>Juegos</p>
              <table className={styles.table}>
                <thead>
                  <tr>
                    <th>Juego</th>
                    <th>RTP (%)</th>
                  </tr>
                </thead>
                <tbody>
                  {data.games.map((row) => (
                    <tr key={row.game}>
                      <td>{row.game}</td>
                      <td>{row.rtp.toFixed(2)}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>

            <div className={styles.section}>
              <p className={styles.sectionTitle}>Usuarios</p>
              <table className={styles.table}>
                <thead>
                  <tr>
                    <th>Usuario ID</th>
                    <th>Total Apostado</th>
                    <th>Total Ganado</th>
                  </tr>
                </thead>
                <tbody>
                  {data.users.map((row) => (
                    <tr key={row.userId}>
                      <td>{row.userId}</td>
                      <td>{row.totalStake}</td>
                      <td>{row.totalWin}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          </>
        )}
      </div>
    </div>
  )
}

export default StatsPage

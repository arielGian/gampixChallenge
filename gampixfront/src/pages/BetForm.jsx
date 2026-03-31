import { useState } from 'react'
import { useMutation } from '@tanstack/react-query'
import { createBet } from '../api/betsApi'
import styles from './BetForm.module.css'

const INITIAL_FORM = {
  userId: '',
  game: '',
  stake: '',
  winAmount: '',
}

const BetForm = () => {
  const [form, setForm] = useState(INITIAL_FORM)

  const mutation = useMutation({
    mutationFn: createBet,
    onSuccess: () => {
      setForm(INITIAL_FORM)
    },
  })

  const handleChange = (e) => {
    const { name, value } = e.target
    setForm((prev) => ({ ...prev, [name]: value }))
  }

  const handleSubmit = (e) => {
    e.preventDefault()
    mutation.mutate({
      userId: Number(form.userId),
      game: form.game,
      stake: Number(form.stake),
      winAmount: Number(form.winAmount),
    })
  }

  const errorMessage =
    mutation.error?.response?.data?.message ??
    mutation.error?.response?.data ??
    mutation.error?.message ??
    'Ocurrio un error al crear la apuesta.'

  return (
    <div className={styles.wrapper}>
      <div className={styles.card}>
        <h1 className={styles.title}>Nueva Apuesta</h1>

        <form onSubmit={handleSubmit} noValidate>
          <div className={styles.fieldGroup}>
            <div className={styles.field}>
              <label htmlFor="userId" className={styles.label}>
                ID de Usuario
              </label>
              <input
                id="userId"
                name="userId"
                type="number"
                min="1"
                placeholder="Ej: 1"
                value={form.userId}
                onChange={handleChange}
                className={styles.input}
                required
              />
            </div>

            <div className={styles.field}>
              <label htmlFor="game" className={styles.label}>
                Partido / Evento
              </label>
              <input
                id="game"
                name="game"
                type="text"
                placeholder="Ej: Roullete , Blackjack"
                value={form.game}
                onChange={handleChange}
                className={styles.input}
                required
              />
            </div>

            <div className={styles.field}>
              <label htmlFor="stake" className={styles.label}>
                Monto apostado
              </label>
              <input
                id="stake"
                name="stake"
                type="number"
                min="0.01"
                step="0.01"
                placeholder="Ej: 100"
                value={form.stake}
                onChange={handleChange}
                className={styles.input}
                required
              />
            </div>

            <div className={styles.field}>
              <label htmlFor="winAmount" className={styles.label}>
                Ganancia potencial
              </label>
              <input
                id="winAmount"
                name="winAmount"
                type="number"
                min="0.01"
                step="0.01"
                placeholder="Ej: 250"
                value={form.winAmount}
                onChange={handleChange}
                className={styles.input}
                required
              />
            </div>
          </div>

          <button
            type="submit"
            className={styles.button}
            disabled={mutation.isPending}
          >
            {mutation.isPending ? 'Enviando...' : 'Aceptar'}
          </button>
        </form>

        {mutation.isSuccess && (
          <p className={styles.success}>Apuesta creada con exito</p>
        )}

        {mutation.isError && (
          <p className={styles.error}>{String(errorMessage)}</p>
        )}
      </div>
    </div>
  )
}

export default BetForm

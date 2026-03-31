import { BrowserRouter, Routes, Route, NavLink } from 'react-router-dom'
import BetForm from './pages/BetForm'
import StatsPage from './pages/StatsPage'

const NAV_LINKS = [
  { to: '/', label: 'Apuestas', end: true },
  { to: '/stats', label: 'Estadísticas' },
]

const navStyle = {
  display: 'flex',
  gap: '1rem',
  justifyContent: 'center',
  padding: '1rem',
  borderBottom: '1px solid #2e2e2e',
  backgroundColor: '#1a1a1a',
}

const linkStyle = ({ isActive }) => ({
  color: isActive ? '#646cff' : '#a0a0a0',
  textDecoration: 'none',
  fontWeight: 700,
  fontSize: '0.9rem',
  letterSpacing: '0.05em',
  textTransform: 'uppercase',
  transition: 'color 0.2s',
})

function App() {
  return (
    <BrowserRouter>
      <nav style={navStyle}>
        {NAV_LINKS.map(({ to, label, end }) => (
          <NavLink key={to} to={to} end={end} style={linkStyle}>
            {label}
          </NavLink>
        ))}
      </nav>
      <Routes>
        <Route path="/" element={<BetForm />} />
        <Route path="/stats" element={<StatsPage />} />
      </Routes>
    </BrowserRouter>
  )
}

export default App

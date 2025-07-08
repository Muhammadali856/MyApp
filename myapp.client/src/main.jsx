import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './Components/App/App.jsx'
import GetDachaId from './Components/GetDachaId/GetDachaId.jsx'

createRoot(document.getElementById('root')).render(
  <StrictMode>
    <GetDachaId/>
  </StrictMode>,
)

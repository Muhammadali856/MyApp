import React from 'react'
import { HashRouter as Router, Routes, Route } from 'react-router-dom'
import GetAllDachas from './Pages/GetAllDachas/GetAllDachas.jsx'
import GetDachaById from './Pages/GetDachaById/GetDachaById.jsx'
import CreateNewDacha from './Pages/CreateNewDacha/CreateNewDacha.jsx'
import GetDachaByName from './Pages/GetDachaByName/GetDachaByName.jsx'
import { Layout } from './Layout.jsx'

const App = () => {
  return (
    <Router>
      <Routes>
        <Route element={<Layout/>}>
          <Route path='/' element={<GetAllDachas/>}/>
          <Route path='/GetDachaById' element={<GetDachaById/>}/>
          <Route path='/CreateNewDacha' element={<CreateNewDacha/>}/>
          <Route path='/GetDachaByName' element={<GetDachaByName/>}/>
        </Route>
      </Routes>
    </Router>
)
}

export default App
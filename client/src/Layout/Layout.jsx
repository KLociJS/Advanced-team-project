import { Outlet } from 'react-router-dom'
import { NavBar } from 'Layout'

export default function Layout() {
  return (
    <>
       <NavBar />
       <main className='container'>
        <Outlet/>
       </main>
    </>
  )
}

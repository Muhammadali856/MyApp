import { Navbar } from "./Components/Navbar"
import { Outlet } from "react-router-dom";

export function Layout() {
    return(
        <div style={{ minHeight: '100vh', display: 'flex', flexDirection: 'column' }}>
            <Navbar />
            <main style={{ flex: 1 }}>
                <Outlet />
            </main>
        </div>
    )
}
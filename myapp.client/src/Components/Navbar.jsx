// Navbar.jsx
import { Link } from 'react-router-dom';
import './Navbar.css';

export function Navbar(){
    return (
        <nav className="navbar-container">
            <Link to="/">
                <button className="link-button">
                    Get All Dachas
                </button>
            </Link>
            <Link to="/GetDachaByName">
                <button className="link-button">
                    Get Dacha By Name
                </button>
            </Link>
            <Link to="/CreateNewDacha">
                <button className="link-button">
                    Create New Dacha
                </button>
            </Link>
        </nav>
    )
}
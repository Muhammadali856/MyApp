// Navbar.jsx
import { Link } from 'react-router-dom';
import './Navbar.css'; // Import the CSS file

export function Navbar(){
    return (
        <nav className="navbar-container"> {/* Apply the container class here */}
            <Link to="/">
                <button className="link-button"> {/* Add class for button styling */}
                    Get All Dachas
                </button>
            </Link>
            <Link to="/GetDachaById">
                <button className="link-button"> {/* Add class for button styling */}
                    Get Dacha By Id
                </button>
            </Link>
            <Link to="/CreateNewDacha">
                <button className="link-button"> {/* Add class for button styling */}
                    Create New Dacha
                </button>
            </Link>
        </nav>
    )
}
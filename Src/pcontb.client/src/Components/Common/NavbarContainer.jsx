import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import { NavLink, Link } from 'react-router-dom';

function NavbarContainer() {
	return (
		<Navbar expand="lg" bg="dark" data-bs-theme="dark">
			<Container>
				<Navbar.Brand>
					<Link to="/home">React-Bootstrap</Link>
				</Navbar.Brand>
				<Navbar.Toggle aria-controls="basic-navbar-nav" />
				<Navbar.Collapse id="basic-navbar-nav">
					<Nav className="me-auto">
						<NavLink to="/home">Home</NavLink>
						<NavLink to="/dashboard">Link</NavLink>
					</Nav>
				</Navbar.Collapse>
			</Container>
		</Navbar>
	);
}

export default NavbarContainer;

import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';

import { NavLink, Link } from 'react-router-dom';

function NavbarContainer() {
	const handleLoginSingUpModalOpen = () => {
		alert('Login/Sign up open');
	};

	const handleSearchPopUpOpen = () => {
		alert('Search pop/up open');
	};

	const handleSearch = e => {
		e.preventDefault();
		console.log(e.target.value);
	};

	return (
		<Navbar expand="lg" bg="dark" data-bs-theme="dark">
			<Container>
				<Navbar.Brand as={Link} to="/">
					PCONTB
				</Navbar.Brand>
				<Navbar.Toggle aria-controls="basic-navbar-nav" />
				<Navbar.Collapse id="basic-navbar-nav">
					<Nav>
						<Nav.Link as={NavLink} to="/explore">
							Explore
						</Nav.Link>
					</Nav>
					<Form
						className="d-flex m-auto w-50"
						onClick={handleSearchPopUpOpen}
						onChange={handleSearch}>
						<Form.Control
							type="search"
							placeholder="Search"
							className="me-2 "
							aria-label="Search"
						/>
					</Form>
					<Nav>
						<Nav.Link onClick={handleLoginSingUpModalOpen} className="d-flex">
							Login/Sign Up
						</Nav.Link>
					</Nav>
					<Button variant="primary">Start Project</Button>
				</Navbar.Collapse>
			</Container>
		</Navbar>
	);
}

export default NavbarContainer;

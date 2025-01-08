import NavbarContainer from './Components/Common/NavbarContainer';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Home from './Views/Home/HomeView';
import Explore from './Views/Dashboard/Explore';

function App() {
	return (
		<Router>
			<NavbarContainer />
			<Routes>
				<Route path="" element={<Home />} />
				<Route path="explore" element={<Explore />} />
			</Routes>
		</Router>
	);
}

export default App;

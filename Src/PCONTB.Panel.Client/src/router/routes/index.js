import Home from "./home";
import Account from "./account";
import Panel from "./panel";
import Projects from "./projects";

let routes = [];

routes.push(...Home);
routes.push(...Account);
routes.push(...Panel);
routes.push(...Projects);

export default routes;

import { Link } from "react-router-dom";
import { useQuery } from "../Utils/Utils";

const BadRequest = () => {
    let query = useQuery(), redirectTo = query.get('redirectTo') ?? '/';

    return (
        <>
            <div id="notfound">
                <div class="notfound">
                    <div class="notfound-404">
                        <h1>400</h1>
                    </div>
                    <h2>Uh oh... Your browser sent something I don't understand.</h2>
                    <form class="notfound-search">
                        <input type="text" placeholder="Search..." />
                        <button type="button">Search</button>
                    </form>
                    <a href="/"><span class="arrow"></span>Return To Homepage</a>
                </div>
            </div>
        </>
    );
}

export default BadRequest;
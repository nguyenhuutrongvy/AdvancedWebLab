const NotFound = () => {
    return (
        <>
            <div id="notfound">
                <div class="notfound">
                    <div class="notfound-404">
                        <h1>404</h1>
                    </div>
                    <h2>Oops, The Page you are looking for can't be found!</h2>
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

export default NotFound;
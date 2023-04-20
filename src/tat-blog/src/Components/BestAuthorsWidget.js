import { useState, useEffect } from "react";
import ListGroup from "react-bootstrap/ListGroup";
import { Link } from "react-router-dom";
import { getBestAuthors } from '../Services/Widgets'

const BesAuthorsWidget = () => {
    const [authorList, setAuthorList] = useState([]);

    useEffect(() => {
        getBestAuthors(4).then(data => {
            if (data) {
                setAuthorList(data);
            } else {
                setAuthorList([]);
            }
        });
    }, [])

    return (
        <div className="mb-4">
            <h3 className="text-success mb-2">
                Top tác giả nổi bật
            </h3>
            {
                authorList.length > 0 &&
                <ListGroup>
                    {
                        authorList.map((item, index) => {
                            return (
                                <ListGroup.Item key={index}>
                                    <Link to={`/blog/author?slug=${item.urlSlug}`}
                                        title={item.fullName}
                                        key={index}
                                        className="text-decoration-none">
                                        {item.fullName}
                                        <span>&nbsp;({item.postCount})</span>
                                    </Link>
                                </ListGroup.Item>
                            );
                        })
                    }
                </ListGroup>
            }
        </div>
    );
}

export default BesAuthorsWidget;
import { useState, useEffect } from "react";
import ListGroup from "react-bootstrap/ListGroup";
import { Link } from "react-router-dom";
import { getFeaturedPosts } from '../Services/Widgets'

const FeaturedPostsWidget = () => {
    const [postList, setPostList] = useState([]);

    useEffect(() => {
        getFeaturedPosts(3).then(data => {
            if (data) {
                setPostList(data);
            } else {
                setPostList([]);
            }
        });
    }, [])

    return (
        <div className="mb-4">
            <h3 className="text-success mb-2">
                Top bài viết được xem nhiều nhất
            </h3>
            {
                postList.length > 0 &&
                <ListGroup>
                    {
                        postList.map((item, index) => {
                            let postedDate = new Date(item.postedDate);

                            return (
                                <ListGroup.Item key={index}>
                                    <Link to={`/blog/post/${postedDate.getFullYear()}/${postedDate.getMonth() + 1}/${postedDate.getDate()}/${item.urlSlug}`}
                                        title={item.title}
                                        key={index}
                                        className="text-decoration-none">
                                        {item.title}
                                        <span>&nbsp;({item.viewCount})</span>
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

export default FeaturedPostsWidget;
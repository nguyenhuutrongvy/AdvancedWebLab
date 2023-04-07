import { useState, useEffect } from "react";
// import ListGroup from "react-bootstrap/ListGroup";
// import { Link } from "react-router-dom";
import { getTags } from '../Services/Widgets'
import TagList from "./TagList";

const TagCloudWidget = () => {
    const [tagList, setTagList] = useState([]);

    useEffect(() => {
        getTags().then(data => {
            if (data) {
                setTagList(data);
            } else {
                setTagList([]);
            }
        });
    }, [])

    return (
        <div className="mb-4">
            <h3 className="text-success mb-2">
                Tag cloud
            </h3>
            <TagList tagList={tagList}></TagList>
        </div>
    );
}

export default TagCloudWidget;
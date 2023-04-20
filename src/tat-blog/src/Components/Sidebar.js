import React from "react";
import SearchForm from './SearchForm';
import CategoriesWidget from './CategoriesWidget';
import FeaturedPostsWidget from './FeaturedPostsWidget';
import SubscribeForm from './SubscribeForm';
import RandomPostsWidget from './RandomPostsWidget';
import TagCloudWidget from './TagCloudWidget';
import BestAuthorsWidget from './BestAuthorsWidget';
import ArchivesWidget from './ArchivesWidget';

const Sidebar = () => {
    return (
        <div className="pt-4 ps-2">
            <SearchForm />
            <CategoriesWidget />
            <FeaturedPostsWidget />
            <SubscribeForm />
            <RandomPostsWidget />
            <TagCloudWidget />
            <BestAuthorsWidget />
            <ArchivesWidget />
        </div>
    )
}

export default Sidebar;
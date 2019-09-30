import { Component, OnInit, Input } from '@angular/core';
import { CreateEditProductData } from 'src/app/models/products/CreateEditProductData';
import { ManageCategoriesService } from 'src/app/features/manage-categories/services/manage-categories.service';
import { CategoryManageHeaderData } from 'src/app/models/categories/CategoryManageHeaderData';
import { ManageCategoryFilterData } from 'src/app/models/categories/ManageCategoryFilterData';
import { PaginationParams } from 'src/app/models/search/PaginationParams';
import { ManageBrandsService } from 'src/app/features/manage-brands/services/manage-brands-service';
import { ManageBrandFilterData } from 'src/app/models/brands/ManageBrandFilterData';
import { BrandManageHeaderData } from 'src/app/models/brands/BrandManageHeaderData';
import { HelperService } from 'src/app/features/search/services/search-helper.service';
import { SearchCategoryAttributeData } from 'src/app/models/categories/search/SearchCategoryAttributeData';
import { AttributeType } from 'src/app/models/categories/AttributeType';
import { CreateEditIntAttribute } from 'src/app/models/products/CreateEditIntAttribute';
import { CreateEditStringAttribute } from 'src/app/models/products/CreateEditStringAttribute';

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.scss']
})
export class ProductFormComponent implements OnInit {

  @Input()
  product: CreateEditProductData;
  categories: CategoryManageHeaderData[];
  brands: BrandManageHeaderData[];
  attributes: SearchCategoryAttributeData[] = [];

  constructor(
    private manageCategoriesService: ManageCategoriesService,
    private manageBrandsService: ManageBrandsService,
    private helperService: HelperService
  ) {
  }

  ngOnInit() {
    this.manageCategoriesService
      .getCategories(new ManageCategoryFilterData(), new PaginationParams())
      .subscribe(result => {
        this.categories = result;
      });

    this.manageBrandsService
      .getBrands(new ManageBrandFilterData(), new PaginationParams())
      .subscribe(result => {
        this.brands = result;
      });

    if (this.product.id != null) {
      this.onCategoryChange(this.product.categoryId);
    }
  }

  onCategoryChange(categoryId) {
    this.helperService.getAttributesOf(categoryId)
      .subscribe(result => {
        this.attributes = result;
        if (this.product.id == null) {
          this.product.stringAttributes = [];
          this.product.intAttributes = [];
        }
        else {
          //Load attr values to form   
          this.loadIntAttributes(this.product.intAttributes);
          this.loadStringAttributes(this.product.stringAttributes);
        }
      });
  }

  selectAttributeValue(attributeId: string, type: AttributeType, event) {
    var attributeValueId = event.target.value;
    if (attributeValueId != "-1") {
      //Add value to product's attribute list
      if (type == AttributeType.Int) {
        var productAttrInt = this.product.intAttributes.filter(e => e.attributeId == attributeId)[0];
        if (productAttrInt) {
          productAttrInt.valueId = attributeValueId;
        }
        else {
          var newAttr: CreateEditIntAttribute = {
            attributeId: attributeId,
            attributeName: null,
            valueId: attributeValueId,
            value: null
          };
          this.product.intAttributes.push(newAttr);
        }
      }
      if (type == AttributeType.String) {
        var productAttrString = this.product.stringAttributes.filter(e => e.attributeId == attributeId)[0];
        if (productAttrString) {
          productAttrString.valueId = attributeValueId;
        }
        else {
          var newAttr2: CreateEditStringAttribute = {
            attributeId: attributeId,
            attributeName: null,
            valueId: attributeValueId,
            value: null
          };
          this.product.stringAttributes.push(newAttr2);
        }
      }
    }
    else {
      //Remove value from product's attribute list
      this.removeAttr(attributeId);
    }
  }

  changeAttributeValue(attributeId: string, type: AttributeType, event) {
    var attributeValue = event.target.value;
    if (attributeValue != "") {
      //Add value to product's attribute list
      if (type == AttributeType.Int) {
        var productAttrInt = this.product.intAttributes.filter(e => e.attributeId == attributeId)[0];
        if (productAttrInt) {
          productAttrInt.value = attributeValue;
        }
        else {
          var newAttr: CreateEditIntAttribute = {
            attributeId: attributeId,
            attributeName: null,
            value: attributeValue,
            valueId: null
          };
          this.product.intAttributes.push(newAttr);
        }
      }
      if (type == AttributeType.String) {
        var productAttrString = this.product.stringAttributes.filter(e => e.attributeId == attributeId)[0];
        if (productAttrString) {
          productAttrString.value = attributeValue;
        }
        else {
          var newAttr2: CreateEditStringAttribute = {
            attributeId: attributeId,
            attributeName: null,
            valueId: null,
            value: attributeValue
          };
          this.product.stringAttributes.push(newAttr2);
        }
      }
    }
    else {
      //Remove value from product's attribute list
      this.removeAttr(attributeId);
    }
  }

  removeAttr(attributeId) {
    this.product.stringAttributes =
      this.product.stringAttributes.filter(e => e.attributeId != attributeId);
    this.product.intAttributes =
      this.product.intAttributes.filter(e => e.attributeId != attributeId);
  }

  loadIntAttributes(intAttributes: CreateEditIntAttribute[]) {
    intAttributes.forEach(attr => {
      var formAttr = this.attributes.filter(e => e.attributeId == attr.attributeId)[0];     
      formAttr.value = attr.value.toString();
      formAttr.valueId = attr.valueId;
    });
  }

  loadStringAttributes(stringAttributes: CreateEditStringAttribute[]) {
    stringAttributes.forEach(attr => {    
      var formAttr2 = this.attributes.filter(e => e.attributeId == attr.attributeId)[0];
      formAttr2.value = attr.value.toString();
      formAttr2.valueId = attr.valueId;
    });
  }
}


//
//  DOXNativeBridge.m
//  Unity-iPhone
//
//  Created by Woncheol Heo on 2019. 6. 19..
//

#import <Foundation/Foundation.h>
#import "DOX.h"
#import "DOXNativeBridge.h"


@interface DoxNativeBridge : NSObject

@end


XProperties *properties;
NSDictionary *setDictionary;
XIdentify *identify;
@implementation DoxNativeBridge

- (instancetype)init {
    return self;
}

//XIdentify *convertToDoxIdentify(IdentifyIos identifyIos) {
//    XIdentify *identify = [[XIdentify alloc] init];
//
//}

XEvent *convertToDoxEvent(EventIos eventIos)
{
    XEvent *event = [[XEvent alloc] init];
    
    if(eventIos.evtName) {
        event.evtname = DoxCreateNSString(eventIos.evtName);
    }
    [event setProperties:properties];
    return event;
}

XConversion *convertToDoxConversion(ConversionIos conversionIos)
{
    XConversion *conversion = [[XConversion alloc] init];
    
    if(conversionIos.cvrName) {
        conversion.cvrname = DoxCreateNSString(conversionIos.cvrName);
    }
    [conversion setProperties:properties];
    return conversion;
}

XProperties *convertToDoxProperties(const char* Keys[], const char* Values[], int propertiesCnt) {
    if(propertiesCnt > 0) {
        for(int i = 0; i < propertiesCnt; i++) {
            [properties set:DoxCreateNSString(Keys[i])  value:DoxCreateNSString(Values[i])];
        }
    }
    
    return properties;
    
}

XProperties *convertToDoxPropertiesByString(const char* key, const char* value) {
    [properties set:DoxCreateNSString(key) value:DoxCreateNSString(value)];
    
    return properties;
}

XProperties *convertToDoxPropertiesByIntArray(const char* key, int values[], int count) {
    for(int i = 0; i < count; i++) {
        NSLog(@"convertToDoxPropertiesByIntArray %d", values[i]);
    }
    //[properties set:DoxCreateNSString(key) value:@"111"];
    
    return properties;
}

XProperties *convertToDoxPropertiesByStringArray(const char* key, const char* values[]) {
    //[properties set:DoxCreateNSString(key) value:values];
    
    return properties;
}

void setSetDictionay(const char* key, const char* value) {
    [identify set:DoxCreateNSString(key) value:DoxCreateNSString(value)];
}

void setSetOnceDictionay(const char* key, const char* value) {
    [identify setOnce:DoxCreateNSString(key) value:DoxCreateNSString(value)];
}

// Converts C style string to NSString
NSString* DoxCreateNSString (const char* string)
{
    
    NSLog(@"parameter: %s", string);
    NSLog(@"converting: %@", [NSString stringWithUTF8String:string ?: ""]);
    return [NSString stringWithUTF8String:string ?: ""];
    //NSString* justName = [[NSString alloc] initWithUTF8String:string]; // -- convert from C style to Objective C style.
}

void printTest(NSString *str) {
    NSLog(@"==== in printTest : %@", str);
}
extern "C" { // -- we define our external method to be in C.
    
    void CallInit( ){
        [DOX initialization]; // -- call method to plugin class
        
        properties = [[XProperties alloc] init];
        setDictionary = [[NSDictionary alloc] init];
        identify = [[XIdentify alloc] init];
    }
    
    void CallSetUserId(const char* userIdChar) {
        [DOX setUserId:DoxCreateNSString(userIdChar)];
    }
    
    void CallSetPushId(const char* pushId) {
        [DOX setUserId:DoxCreateNSString(pushId)];
    }
    
    void CallSetDeepLink(const char* deepLink) {
        [DOX setUserId:DoxCreateNSString(deepLink)];
    }
    
    void CallLogEvent(const char* eventName, const char* xPropertiesString) {
        NSString *xProperties = DoxCreateNSString(xPropertiesString);
        NSData *data = [xProperties dataUsingEncoding:NSUTF8StringEncoding];
        NSDictionary *propertiesDict = [NSJSONSerialization JSONObjectWithData:data options:NSJSONReadingMutableContainers error:nil];
        
        NSDictionary *eventDictionary = [[NSMutableDictionary alloc] init];
        [eventDictionary setValue:DoxCreateNSString(eventName) forKey:@"evtName"];
        [eventDictionary setValue:propertiesDict forKey:@"properties"];
        
        [DOX logEventWith:eventDictionary];
    }
    
    void CallLogConversion(const char* conversionName, const char* xPropertiesString) {
        NSString *xProperties = DoxCreateNSString(xPropertiesString);
        NSData *data = [xProperties dataUsingEncoding:NSUTF8StringEncoding];
        NSDictionary *propertiesDict = [NSJSONSerialization JSONObjectWithData:data options:NSJSONReadingMutableContainers error:nil];
        
        NSDictionary *conversionDictionary = [[NSMutableDictionary alloc] init];
        [conversionDictionary setValue:DoxCreateNSString(conversionName) forKey:@"cvrName"];
        [conversionDictionary setValue:propertiesDict forKey:@"properties"];
        
        [DOX logConversionWith:conversionDictionary];
    }
    
    
    void CallLogRevenue(const char* orderNo, const char* revenueType ,const char* currency, const char* xProductsString, const char* xPropertiesString) {
        NSString *xProperties = DoxCreateNSString(xPropertiesString);
        NSData *data = [xProperties dataUsingEncoding:NSUTF8StringEncoding];
        NSDictionary *propertiesDict = [NSJSONSerialization JSONObjectWithData:data options:NSJSONReadingMutableContainers error:nil];
        
        NSString *productsString = DoxCreateNSString(xProductsString);
        data = [productsString dataUsingEncoding:NSUTF8StringEncoding];
        NSArray *productList = [NSJSONSerialization JSONObjectWithData:data options:NSJSONReadingMutableContainers error:nil];
        
        NSMutableArray *processedProductList = [[NSMutableArray alloc] init];
        
        for(int i = 0; i < productList.count; i++) {
            NSDictionary *productDictionary = [[NSDictionary alloc] init];
            productDictionary = (NSDictionary *)[productList objectAtIndex:i];
            if ( [productDictionary objectForKey:@"properties"] ) {
                NSString *xPropertiesInProduct = [productDictionary objectForKey:@"properties"];
                NSData *data = [xPropertiesInProduct dataUsingEncoding:NSUTF8StringEncoding];
                NSDictionary *propertiesDictInProduct = [NSJSONSerialization JSONObjectWithData:data options:NSJSONReadingMutableContainers error:nil];
                [productDictionary setValue:propertiesDictInProduct forKey:@"properties"];
            }
            [processedProductList addObject:productDictionary];
        }
        NSDictionary *revenueDictionary = [[NSMutableDictionary alloc] init];
        [revenueDictionary setValue:DoxCreateNSString(orderNo) forKey:@"ordNo"];
        [revenueDictionary setValue:DoxCreateNSString(revenueType) forKey:@"eventType"];
        [revenueDictionary setValue:DoxCreateNSString(currency) forKey:@"curcy"];
        [revenueDictionary setValue:processedProductList forKey:@"products"];
        [revenueDictionary setValue:propertiesDict forKey:@"properties"];
        
        [DOX logRevenueWith:revenueDictionary];
        
    }
    
    void CallUserIdentify(const char* xIdentifyString) {
        NSString *identify = DoxCreateNSString(xIdentifyString);
        
        NSData *data = [identify dataUsingEncoding:NSUTF8StringEncoding];
        NSDictionary *identifyDict = [NSJSONSerialization JSONObjectWithData:data options:NSJSONReadingMutableContainers error:nil];
        
        [DOX userIdentifyWith:identifyDict];
    }
    
    void CallGroupIdentify(const char* groupString, const char* xIdentifyString) {
        
        NSString *group = DoxCreateNSString(groupString);
        NSData *data = [group dataUsingEncoding:NSUTF8StringEncoding];
        NSDictionary *groupDict = [NSJSONSerialization JSONObjectWithData:data options:NSJSONReadingMutableContainers error:nil];
        
        NSString *identify = DoxCreateNSString(xIdentifyString);
        
        data = [identify dataUsingEncoding:NSUTF8StringEncoding];
        NSDictionary *identifyDict = [NSJSONSerialization JSONObjectWithData:data options:NSJSONReadingMutableContainers error:nil];
        
        [DOX groupIdentifyWith:groupDict identify:identifyDict];
        
    }
    
    void makeSetDictionary(const char* key, const char* value) {
        setSetDictionay(key, value);
    }
    
    void makeSetOnceDictionary(const char* key, const char* value) {
        setSetOnceDictionay(key, value);
    }
    
    void makeXProperties(const char* Keys[], const char* Values[], int propertiesCnt) {
        properties = convertToDoxProperties(Keys, Values, propertiesCnt);
    }
    
    void makeXPropertiesByString(const char* key, const char* value) {
        properties = convertToDoxPropertiesByString(key, value);
    }
    
    void makeXPropertiesByIntArray(const char* key, int values[], int count) {
        properties = convertToDoxPropertiesByIntArray(key, values, count);
    }
    
    void makeXPropertiesByStringArray(const char* key, const char* values[]) {
        properties = convertToDoxPropertiesByStringArray(key, values);
    }
    
    
}


@end










